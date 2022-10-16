using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LawnWizardApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LawnWizardApp.Controllers;

public class HomeController : Controller
{
    private MyContext db;

    public HomeController(MyContext context)
    {
        db = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("dashboard")]
    public IActionResult Dashboard()
    {
        Employee? loggedEmployee = db.Employees.FirstOrDefault(e => e.EmployeeId == HttpContext.Session.GetInt32("employeeId"));

        if (loggedEmployee != null)
        {
            ViewBag.Employee = loggedEmployee;
            return View();
        }
        return RedirectToAction("Index", "Home");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
