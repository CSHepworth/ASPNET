using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DojoSurveyModel.Models;

namespace DojoSurveyModel.Controllers;

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

    [HttpPost("Results")]
    public IActionResult ShowResults(Survey yourSurvey)
    {
        if (ModelState.IsValid)
        {
            return View("Results", yourSurvey);
        }
        else
        {
            return View("Index");
        }
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