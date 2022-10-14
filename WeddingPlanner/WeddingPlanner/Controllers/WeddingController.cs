using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WeddingPlanner.Controllers;

public class WeddingController : Controller
{
    private MyContext db;

    public WeddingController(MyContext context)
    {
        db = context;
    }

    [HttpGet("planwedding")]
    public IActionResult PlanWedding()
    {
        User? loggedinUser = db.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("userId"));

        if (loggedinUser != null)
        {
            ViewBag.User = loggedinUser;
            return View();
        }
        return RedirectToAction("Index", "Home");
    }

    [HttpPost("createwedding")]
    public IActionResult CreateWedding(Wedding wedding)
    {
        if(ModelState.IsValid)
        {
            int? sessionid = HttpContext.Session.GetInt32("userId");
            if(sessionid != null)
            {
                wedding.UserId = (int)sessionid;
                db.Weddings.Add(wedding);
                db.SaveChanges();
                return RedirectToAction("Dashboard", "User");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        return RedirectToAction("planwedding");
    }

    [HttpGet("/view/{weddingId}")]
    public IActionResult ViewWedding(int weddingId)
    {
        User? loggedInUser = db.Users.FirstOrDefault(u=> u.UserId == HttpContext.Session.GetInt32("userId"));

        if (loggedInUser != null)
        {
            ViewBag.User = loggedInUser;
            Wedding? wedding = db.Weddings
            .Include(w => w.GuestList)
            .FirstOrDefault(w => w.WeddingId == weddingId);
            return View("ViewWedding", wedding);
        }
        return RedirectToAction("Dashboard", "User");
    }

}