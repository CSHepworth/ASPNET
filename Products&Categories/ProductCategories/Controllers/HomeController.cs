using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProductCategories.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace ProductCategories.Controllers;

public class HomeController : Controller
{
    private MyContext dbcontext;

    public HomeController(MyContext context)
    {
        dbcontext = context;
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
