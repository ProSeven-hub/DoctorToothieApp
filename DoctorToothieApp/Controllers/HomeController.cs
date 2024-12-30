using System.Diagnostics;
using System.Security.Claims;
using DoctorToothieApp.DbModels;
using DoctorToothieApp.Interfaces;
using DoctorToothieApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctorToothieApp.Controllers;

class ScheduleVisitVM
{
    public List<Room> Rooms { get; set; } = [];
    public List<Reservation> Reservations { get; set; } = [];

    public IEnumerable<Reservation> NotInProgress()
    {
        return Reservations.Where(e => new List<ReservationStage> { ReservationStage.COMPLETED, ReservationStage.CANCELED }.Contains(e.Stage));
    }
    public Reservation? InProgress =>
        Reservations.SingleOrDefault(e => !new List<ReservationStage> { ReservationStage.COMPLETED, ReservationStage.CANCELED }.Contains(e.Stage));
    
}

public class HomeController(ILogger<HomeController> logger, IDbContext dbContext) : Controller
{
    public async Task<IActionResult> Index()
    {
        logger.LogInformation("test");
        logger.LogInformation($"{nameof(HomeController)}");

        List<Room> outCount = (await dbContext.Rooms.Include(e => e.Parent).ToListAsync()) ?? [];

        return View(outCount);
    }

    public async Task<IActionResult> ScheduleVisit()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return View(new ScheduleVisitVM());
        }

        ScheduleVisitVM scheduleVisitVM = new ();
        scheduleVisitVM.Rooms = (await dbContext.Rooms.Include(e => e.Parent).ToListAsync()) ?? [];
        scheduleVisitVM.Reservations = (await dbContext.Reservations
            .Include(e => e.Patient)
            .Where( e=> e.Patient != null & e.Patient!.Id == userId)
            .ToListAsync());
        return View(scheduleVisitVM);
    }
    public async Task<IActionResult> ViewVisits()
    {
        List<Room> outCount = (await dbContext.Rooms.Include(e => e.Parent).ToListAsync()) ?? [];
        return View(outCount);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public void LocaitonBtn_Click(object sender, EventArgs e)
    {
        Console.WriteLine("test");
    }
}
