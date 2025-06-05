using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DoctorToothieApp.Persistence.Models;
using DoctorToothieApp.Persistence.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Text.Encodings.Web;
using System.Text;

namespace DoctorToothieApp.Controllers;

public class CreateDoctorViewModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "{0} musi mieć conajmniej {2} i maksymalnie {1} znaków długości.", MinimumLength = 3)]
    [DataType(DataType.Text)]
    [Display(Name = "Imie")]
    public string FirstName { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "{0} musi mieć conajmniej {2} i maksymalnie {1} znaków długości.", MinimumLength = 3)]
    [DataType(DataType.Text)]
    [Display(Name = "Nazwisko")]
    public string LastName { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [Required]
    [StringLength(100, ErrorMessage = "{0} musi mieć conajmniej {2} i maksymalnie {1} znaków długości.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Hasło")]
    public string Password { get; set; }


    [ValidateNever]
    public IList<SelectListItem> Locations { get; set; } = [];

    [Required(ErrorMessage = "Pole {0} jest puste")]
    [DisplayName("Lokalizacja")]
    public int? LocationId { get; set; }
}

public class DoctorsEditVM
{
    [ValidateNever]
    public AppUser Doctor { get; set; }


    [ValidateNever]
    public IList<SelectListItem> Locations { get; set; } = [];

    [HiddenInput]
    public string? DoctorId { get; set; }


    [Required(ErrorMessage = "Pole {0} jest puste")]
    [DisplayName("Lokalizacja")]
    public int? LocationId { get; set; }


}
[Authorize(Roles = "Admin")]
public class DoctorsController(IDbContext context, UserManager<AppUser> userManager) : Controller
{

    public async Task<IActionResult> Index()
    {
        var doctors = await context.Users.Include(e => e.EmployeedLocation).Where(e => e.EmployeedLocationId != null).ToListAsync();
        return View(doctors);
    }
    private async Task<List<SelectListItem>> GetUsers()
    {
        var users = new List<SelectListItem> { new SelectListItem {
            Disabled = true,
            Selected = true,
            Text = "-- Wybierz --"
        } };

        users.AddRange(await context.Users.Select(e => new SelectListItem
        {
            Text = $"{e.FirstName} {e.LastName} - {e.Email}",
            Value = e.Id.ToString()
        }).ToListAsync());

        return users;
    }

    private async Task<List<SelectListItem>> GetLocations()
    {
        var locations = new List<SelectListItem> { new SelectListItem {
            Disabled = true,
            Selected = true,
            Text = "-- Wybierz --"
        } };

        locations.AddRange(await context.Locations.Select(e => new SelectListItem
        {
            Text = $"{e.Name} - {e.Address}",
            Value = e.Id.ToString()
        }).ToListAsync());

        return locations;
    }

    public async Task<IActionResult> New()
    {
        return View(new CreateDoctorViewModel
        {
            Locations = await GetLocations()
            
        });
    }
    private AppUser CreateUser()
    {
        try
        {
            return Activator.CreateInstance<AppUser>();
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(User)}'. " +
                $"Ensure that '{nameof(User)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
        }
    }

    [HttpPost]
    public async Task<IActionResult> New(CreateDoctorViewModel vm)
    {
        vm.Locations = await GetLocations();
        if (ModelState.IsValid)
        {
            var user = CreateUser();
            user.FirstName = vm.FirstName;
            user.LastName = vm.LastName;
            user.UserName = vm.Email;
            

            var result = await userManager.CreateAsync(user, vm.Password);
            await userManager.AddToRoleAsync(user, "Doctor");

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(vm);
            }
            var userId = await userManager.GetUserIdAsync(user);
            var doctor = await context.Users.SingleAsync(e => e.Id == userId);
            doctor.EmployeedLocationId = vm.LocationId;
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(vm);

    }

    public async Task<IActionResult> Edit(string id)
    {
        var doctor = await context.Users.SingleAsync(e => e.Id == id && e.EmployeedLocationId != null);
        return View(new DoctorsEditVM
        {
            Doctor = doctor,
            DoctorId = id,
            LocationId = doctor.EmployeedLocationId,
            Locations = await GetLocations()
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(DoctorsEditVM vm)
    {
        if (!ModelState.IsValid)
        {
            return View(new DoctorsEditVM
            {
                DoctorId = vm.DoctorId,
                LocationId = vm.LocationId,
                Locations = await GetLocations()
            });
        }

        var doctor = await context.Users.SingleAsync(e=>e.Id == vm.DoctorId);
        doctor.EmployeedLocationId = vm.LocationId;
        await context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Remove(string id)
    {
        var doctor = await userManager.FindByIdAsync(id);

        if (doctor != null)
        {
            doctor.EmployeedLocationId = null;
            await userManager.RemoveFromRoleAsync(doctor, "Doctor");
        }

        return RedirectToAction(nameof(Index));
    }

}
