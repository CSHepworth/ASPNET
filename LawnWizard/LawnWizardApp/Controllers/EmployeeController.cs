using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LawnWizardApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace LawnWizardApp.Controllers;

public class EmployeeController : Controller
{
    private MyContext db;

    public EmployeeController(MyContext context)
    {
        db = context;
    }

    

    [HttpPost("loginemployee")]
    public IActionResult LoginEmployee(LoginEmployee employee)
    {
        if(ModelState.IsValid)
        {
            Employee? loginEmployee = db.Employees?.FirstOrDefault(e => e.Email == employee.LoginEmail);

            if(loginEmployee == null)
            {
                ModelState.AddModelError("Email", "Invalid Email/Password");
                return RedirectToAction("Index", "Home");
            }

            PasswordHasher<LoginEmployee> hasher = new PasswordHasher<LoginEmployee>();

            var result = hasher.VerifyHashedPassword(employee, loginEmployee.Password, employee.LoginPassword);

            if(result == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("Password", "Invalid Password");
                return RedirectToAction("Index", "Home");
            }
            HttpContext.Session.SetInt32("employeeId", loginEmployee.EmployeeId);
            return RedirectToAction("Dashboard", "Home");
        }
        return RedirectToAction("Index", "Home");
    }

    [HttpGet("logoutemployee")]
    public IActionResult LogoutEmployee()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet("employees")]
    public IActionResult Employees()
    {
        Employee? loggedEmployee = db.Employees.FirstOrDefault(e => e.EmployeeId == HttpContext.Session.GetInt32("employeeId"));

        if (loggedEmployee == null)
        {
            return RedirectToAction("Index", "Home");
        }
        ViewBag.employee = loggedEmployee;
        List<Employee> allEmployees = db.Employees.ToList();
        return View("Employees", allEmployees);
    }

    [HttpPost("view/{employeeId}")]
    public IActionResult ViewEmployee(Employee employee)
    {
        Employee? viewemployee = db.Employees.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);

        if (viewemployee == null)
        {
            return RedirectToAction("Employees");
        }

        return View("ViewEmployee", viewemployee);
    }
}