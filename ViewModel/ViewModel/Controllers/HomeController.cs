using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Models;

namespace ViewModel.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        string message = "Can't keep gettin away with it";
        return View("Index", message);
    }

    [HttpGet("Numbers")]
    public IActionResult Numbers()
    {
        int [] numbers = new int []
        {
            2,
            1,
            1,
            2
        };
        return View(numbers);
    }

    [HttpGet("Users")]
    public IActionResult Users()
    {
        List<User> users = new List<User>();
        users.Add(new User("Geddy", "Lee"));
        users.Add(new User("Alex", "Lifeson"));
        users.Add(new User("Neil", "Pert"));
        return View(users);
    }

    [HttpGet("User")]
    public IActionResult OneUser()
    {
        User firstUser = new User("Geddy", "Lee");
        return View("User", firstUser);
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
