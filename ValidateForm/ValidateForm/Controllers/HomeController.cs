using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ValidateForm.Models;

namespace ValidateForm.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("submit")]
    public IActionResult Submit(Submitter user)
    {
        if(ModelState.IsValid)
        {
            return RedirectToAction("Success");
        }
        return View("Index");
    }

    [HttpGet("success")]
    public ViewResult Success()
    {
        return View();
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
