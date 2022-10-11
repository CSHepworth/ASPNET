using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChefsNDishes.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace ChefsNDishes.Controllers;

public class HomeController : Controller
{
    private MyContext dbcontext;

    public HomeController(MyContext context)
    {
        dbcontext = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        List<Chef> chefs = dbcontext.Chefs.Include(chef => chef.CreatedDishes).ToList();

        return View(chefs);
    }

    [HttpGet("dishes")]
    public IActionResult Dishes()
    {
        List<Dish> dishwithchef = dbcontext.Dishes
            .Include(dish => dish.Creator)
            .ToList();

        return View(dishwithchef);
    }

    [HttpGet("addchef")]
    public IActionResult AddChef()
    {
        return View();
    }

    [HttpPost("createchef")]
    public IActionResult CreateChef(Chef chef)
    {
        if(ModelState.IsValid)
        {
            if(dbcontext.Chefs.Any(chef => chef.FirstName == chef.FirstName && chef.LastName == chef.LastName))
            {
                ModelState.AddModelError("FirstName LastName", "Name already exists");
            }

            var newChef = dbcontext.Chefs.Add(chef);
            dbcontext.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    [HttpGet("adddish")]
    public IActionResult AddDish()
    {
        ViewBag.AllChefs = dbcontext.Chefs.ToList();
        return View();
    }

    [HttpPost("createdish")]
    public IActionResult CreateDish(Dish dish)
    {
        if(ModelState.IsValid)
        {
            if(dbcontext.Dishes.Any(dish => dish.DishName == dish.DishName))
            {
                ModelState.AddModelError("DishName", "Name already exists");
            }

            var newDish = dbcontext.Dishes.Add(dish);
            dbcontext.SaveChanges();
        }
        return RedirectToAction("Dishes");
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
