using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LawnWizardApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

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

    [HttpPost("registeremployee")]
    public IActionResult RegisterEmployee(Employee newemployee)
    {
        if(ModelState.IsValid)
        {
            if(db.Employees.Any(e => e.Email == newemployee.Email))
            {
                ModelState.AddModelError("Email", "Email is already taken.");
            }

            PasswordHasher<Employee> Hasher = new PasswordHasher<Employee>();
            newemployee.Password = Hasher.HashPassword(newemployee, newemployee.Password);

            db.Employees.Add(newemployee);
            db.SaveChanges();

            HttpContext.Session.SetInt32("employeeId", newemployee.EmployeeId);
            return RedirectToAction("Dashboard");
        }
        return View("Index");
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
