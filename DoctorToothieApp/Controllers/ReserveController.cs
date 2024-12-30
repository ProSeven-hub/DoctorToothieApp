using System.Security.Claims;
using DoctorToothieApp.DbModels;
using DoctorToothieApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DoctorToothieApp.Controllers;

public class ReserveLocationVM
{
    required    public Reservation Reservation { get; set; }
    public IList<SelectListItem> Locations { get; set; } = [];
}

[Authorize]
[Controller]
public class ReserveController(IDbContext context) : Controller
{
    string UserID => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    readonly IList<ReservationStage> Completed = new List<ReservationStage> { ReservationStage.COMPLETED, ReservationStage.CANCELED };

    public async Task<Reservation> GetReservation()
    {
        return await context.Reservations.Where(u => u.PatientId == UserID).SingleAsync(e => !Completed.Contains(e.Stage));
    }

    public async Task UpdateReservation(ReservationStage stage)
    {
        var reservation = await GetReservation();
        reservation.Stage = stage;
        await context.SaveChangesAsync();
    }

    [HttpGet]
    public async Task<IActionResult> Location()
    {
        var res = context.Reservations.Where(u => u.PatientId == UserID);
        var inProgress = await res.SingleOrDefaultAsync(e =>! Completed.Contains(e.Stage));

        var reservation = inProgress ?? new Reservation
        {
            Stage = ReservationStage.LOCATION,
            PatientId = UserID
        };

        if (inProgress == null)
        {
            context.Reservations.Add(reservation);
            await context.SaveChangesAsync();
        }

        var list = new List<SelectListItem>
        {
            new SelectListItem
            {
                Value = null,
                Text = "-- Wybierz --",
                Disabled = true,
                Selected = true
            }
        };
        list.AddRange(await context.Locations.Select(e => new SelectListItem
        {
            Value = e.Id.ToString(),
            Text = e.Name
        }).ToListAsync());

        var vm = new ReserveLocationVM
        {
            Reservation = reservation,
            Locations = list
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> LocationPost([FromForm] int Location)
    {
        var rev = await GetReservation();
        rev.LocationId = Location;
        await context.SaveChangesAsync();

        await UpdateReservation(ReservationStage.ROOM);
        return RedirectToAction(nameof(Room));
    }

    public async Task<IActionResult> Room()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> RoomPost([FromForm] string Location)
    {
        await UpdateReservation(ReservationStage.PATIENT);
        return RedirectToAction(nameof(Patient));
    }

    public async Task<IActionResult> Patient()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> PatientPost([FromForm] string Location)
    {
        await UpdateReservation(ReservationStage.DOCTOR);
        return RedirectToAction(nameof(Doctor));
    }


    public async Task<IActionResult> Doctor()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> DoctorPost([FromForm] string Location)
    {
        await UpdateReservation(ReservationStage.PROCEDURE);
        return RedirectToAction(nameof(Procedure));
    }


    public async Task<IActionResult> Procedure()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> ProcedurePost([FromForm] string Location)
    {
        await UpdateReservation(ReservationStage.DATE);
        return RedirectToAction(nameof(Date));
    }


    public async Task<IActionResult> Date()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> DatePost([FromForm] string Location)
    {
        return RedirectToAction(nameof(Room));
    }


    public IActionResult Review()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> ReviewPost([FromForm] string Location)
    {
        return Redirect("/");
    }


}
