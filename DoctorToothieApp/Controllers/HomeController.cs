using System.Diagnostics;
using DoctorToothieApp.DbModels;
using DoctorToothieApp.Interfaces;
using DoctorToothieApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctorToothieApp.Controllers;

public class HomeController(ILogger<HomeController> logger, IDbContext dbContext) : Controller
{
    public async Task<IActionResult> Index()
    {
        logger.LogInformation("test");
        logger.LogInformation($"{nameof(HomeController)}");

        List<Room> outCount = (await dbContext.Rooms.Include(e => e.Parent).ToListAsync()) ?? [];

        return View(outCount);
    }

    public async Task<IActionResult> Visit()
    {
        List<Room> outCount = (await dbContext.Rooms.Include(e => e.Parent).ToListAsync()) ?? [];
        return View("ScheduleVisit", outCount);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
