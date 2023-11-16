using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Testem.Controllers;
using Testem.Models;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }


    [SessionCheck]
    public IActionResult Dashboard()
    {
        // Get all groups associated with the logged in user
        User? LoggedInUser = _context.Users.Include(u => u.AllMemberships).ThenInclude(m => m.Group).FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
        return View("Dashboard", LoggedInUser);
    }

    [SessionCheck]
    [HttpGet("/privacy")]
    public IActionResult Privacy()
    {
        return View("Privacy");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
