using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DoctorToothieApp.Persistence.Models;
using DoctorToothieApp.Persistence.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DoctorToothieApp.Controllers;


public class RoleVM
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int UsersCount { get; set; }
}

public class RoleUsersVM
{
    public string RoleName { get; set; }
    public List<AppUser> UsersInRole { get; set; }
    public List<AppUser> UsersNotInRole { get; set; }
}
[Authorize(Roles = "Admin")]
public class RolesController(IDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager) : Controller
{
    public async Task<IActionResult> Index()
    {
        var roles = await roleManager.Roles.ToListAsync();
        var roleVMs = new List<RoleVM>();

        foreach (var role in roles)
        {
            var usersInRole = await userManager.GetUsersInRoleAsync(role.Name);
            var usersCount = usersInRole.Count();

            roleVMs.Add(new RoleVM
            {
                Id = role.Id,
                Name = role.Name,
                UsersCount = usersCount
            });
        }

        return View(roleVMs);
    }

    [HttpPost]
    public async Task<IActionResult> Add(string roleName)
    {
        if (!string.IsNullOrWhiteSpace(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var role = await roleManager.FindByIdAsync(id);
        if (role == null || role.Name.ToLower() == "admin") return BadRequest();

        var users = await userManager.GetUsersInRoleAsync(role.Name);
        foreach (var user in users)
        {
            await userManager.RemoveFromRoleAsync(user, role.Name);
        }

        await roleManager.DeleteAsync(role);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> ManageUsers(string roleName)
    {
        var allUsers = userManager.Users.ToList();
        var usersInRole = await userManager.GetUsersInRoleAsync(roleName);
        var usersNotInRole = allUsers.Except(usersInRole).ToList();

        var vm = new RoleUsersVM
        {
            RoleName = roleName,
            UsersInRole = usersInRole.ToList(),
            UsersNotInRole = usersNotInRole
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> AddUserToRole(string userId, string roleName)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user != null && await roleManager.RoleExistsAsync(roleName))
        {
            await userManager.AddToRoleAsync(user, roleName);
        }

        return RedirectToAction(nameof(ManageUsers), new { roleName });
    }

    [HttpPost]
    public async Task<IActionResult> RemoveUserFromRole(string userId, string roleName)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user != null)
        {
            await userManager.RemoveFromRoleAsync(user, roleName);
        }

        return RedirectToAction(nameof(ManageUsers), new { roleName });
    }

}
