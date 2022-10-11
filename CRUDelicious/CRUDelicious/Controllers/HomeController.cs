#pragma warning disable CS8618
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext dbcontext;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        dbcontext = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        ViewBag.AllDishes = dbcontext.Dishes.ToList();

        ViewBag.GuysDishes = dbcontext.Dishes.Where(d => d.Chef == "Guy Fierri").ToList();

        ViewBag.WMK = dbcontext.Dishes.FirstOrDefault(d => d.Name == "Watermellon Koolaid");
        return View("Index");
    }

    [HttpGet("/dishes/new")]
    public IActionResult AddDish()
    {
        return View("AddDish");
    }

    [HttpPost("create")]
    public IActionResult Create(Dish dish)
    {
        var newDish = dbcontext.Dishes.Add(dish).Entity;
        dbcontext.SaveChanges();

        return RedirectToAction("Index");
    }
    
    [HttpGet("/dishes/{dishId}")]
    public IActionResult DisplayDish(int dishId)
    {
        Dish? dish = dbcontext.Dishes.FirstOrDefault(d => d.DishId == dishId);
        return View("DisplayDish", dish);
    }

    [HttpGet("/dishes/{editDishId}/edit")]
    public IActionResult EditDish(int editDishId)
    {
        Dish? dish = dbcontext.Dishes.FirstOrDefault(dish => dish.DishId == editDishId);

        if(dish == null)
        {
            return RedirectToAction("Index");
        }
        return View("EditDish", dish);
    }

    [HttpPost("/dishes/{dishId}/update")]
    public IActionResult Update(Dish editedDish, int dishId)
    {
        Dish? dish = dbcontext.Dishes.FirstOrDefault(d => d.DishId == dishId);
        if (dish == null)
        {
            return RedirectToAction("Index");
        }
        dish.Name = editedDish.Name;
        dish.Chef = editedDish.Chef;
        dish.Calories = editedDish.Calories;
        dish.Tastiness = editedDish.Tastiness;
        dish.Description = editedDish.Description;
        dish.UpdatedAt = DateTime.Now;

        dbcontext.Dishes.Update(dish);
        dbcontext.SaveChanges();
        return RedirectToAction("DisplayDish", new {dishId = dish.DishId});
    }

    [HttpPost("/dishes/{deleteDishId}/delete")]
    public IActionResult DeleteDish(int deleteDishId)
    {
        Dish? dish = dbcontext.Dishes.FirstOrDefault(d => d.DishId == deleteDishId);
        if (dish != null)
        {
            dbcontext.Dishes.Remove(dish);
            dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }
        else
        {
            return RedirectToAction("Index");
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
