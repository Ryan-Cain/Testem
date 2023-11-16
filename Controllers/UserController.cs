using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Identity;
using Testem.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace Testem.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private MyContext _context;
    public UserController(ILogger<UserController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Dashboard()
    {
        
        return View();
    }

    [HttpPost("users/create")]
    public IActionResult CreateUser(User newUser)
    {
        if (ModelState.IsValid)
        {
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
            _context.Add(newUser);
            _context.SaveChanges();
            HttpContext.Session.SetInt32("UserId", newUser.UserId);
            HttpContext.Session.SetString("Username", newUser.FirstName);
            return RedirectToAction("Dashboard");
        }
        else
        {
            return View("Index");
        }
    }
    [HttpPost("users/login")]
    public IActionResult UserLogin(UserLogin userSubmitted)
    {
        if (!ModelState.IsValid)
        {
            return View("Index");
        }
        User? UserInDb = _context.Users.FirstOrDefault(u => u.Email == userSubmitted.LoginEmail);
        if (UserInDb == null)
        {
            ModelState.AddModelError("LoginPassword", "Invalid Email/Password");
            return View("Index");
        }

        PasswordHasher<UserLogin> Hasher = new PasswordHasher<UserLogin>();
        var Result = Hasher.VerifyHashedPassword(userSubmitted, UserInDb.Password, userSubmitted.LoginPassword);

        if (Result == 0)
        {
            ModelState.AddModelError("PasswordLogin", "Invalid Email/Password");
            return View("Index");
        }

        HttpContext.Session.SetInt32("UserId", UserInDb.UserId);
        HttpContext.Session.SetString("Username", UserInDb.FirstName);

        return RedirectToAction("Dashboard");
    }

    [HttpGet("users/logout")]
    public IActionResult UserLogout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }
}

// Name this anything you want with the word "Attribute" at the end
public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Find the session, but remember it may be null so we need int?
        int? userId = context.HttpContext.Session.GetInt32("UserId");
        // Check to see if we got back null
        if (userId == null)
        {
            // Redirect to the Index page if there was nothing in session
            // "Home" here is referring to "HomeController", you can use any controller that is ppropriate here
            context.Result = new RedirectToActionResult("Index", "User", null);
        }
    }
}