// Using statements
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MonsterEntity.Models;
namespace Monsters.Controllers;
    
public class HomeController : Controller
{
    private MyContext _context;
     
    // here we can "inject" our context service into the constructor
    public HomeController(MyContext context)
    {
        _context = context;
    }
     
    [HttpGet("")]
    public IActionResult Index()
    {
        List<Monster> AllMonsters = _context.Monsters.ToList();

        return View();
    }
 }

