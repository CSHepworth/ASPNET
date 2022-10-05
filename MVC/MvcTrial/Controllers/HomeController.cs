using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcTrial.Models;

namespace MvcTrial.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewBag.MyNum = 9;
        int myNum = 12;
        return View(myNum);
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

    [HttpGet("user")]
    public IActionResult AUser()
    {
        User newUser = new User()
        {
            FirstName = "Cooper",
            LastName = "Hepworth"
        };
        return View("User", newUser);
    }
    
}
