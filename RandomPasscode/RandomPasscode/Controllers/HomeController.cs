using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RandomPasscode.Models;
using Microsoft.AspNetCore.Http;

namespace RandomPasscode.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(Generator startpasscode)
    {
        int? oldcount = HttpContext.Session.GetInt32("count");
        if (oldcount == null)
        {
            HttpContext.Session.SetInt32("count", 1);
        }
        return View("Index", startpasscode);
    }

    [HttpGet("generate")]
    public IActionResult Generate(Generator newpasscode)
    {
        int? oldcount = HttpContext.Session.GetInt32("count");
        HttpContext.Session.SetInt32("count", Convert.ToInt32(oldcount += 1));
        return RedirectToAction("Index", newpasscode);
    }

    [HttpGet("reset")]
    public IActionResult ClearSession()
    {
        HttpContext.Session.Clear();
        HttpContext.Session.SetInt32("count", 1);
        return RedirectToAction("Index");
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
