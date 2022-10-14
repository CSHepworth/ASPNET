using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace WeddingPlanner.Controllers;

public class UserController : Controller
{
    private MyContext db;

    public UserController(MyContext context)
    {
        db = context;
    }

    [HttpPost("registeruser")]
    public IActionResult RegisterUser(User user)
    {
        if(ModelState.IsValid)
        {
            if(db.Users.Any(u => u.Email == user.Email))
            {
                ModelState.AddModelError("Email", "Email already in use.");
            }

            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            user.Password = Hasher.HashPassword(user, user.Password);

            db.Users.Add(user);
            db.SaveChanges();

            HttpContext.Session.SetInt32("userId", user.UserId);
            return RedirectToAction("Dashboard");
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpPost("login")]
    public IActionResult Login(LoginUser user)
    {
        if(ModelState.IsValid)
        {
            User? userInDb = db.Users?.FirstOrDefault(u => u.Email == user.LoginEmail);

            if(userInDb == null)
            {
                ModelState.AddModelError("Email", "Invalid Email/Password");
                return RedirectToAction("Index", "Home");
            }

            PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();

            var result = hasher.VerifyHashedPassword(user, userInDb.Password, user.LoginPassword);

            if(result == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("Password", "Invalid Password");
                return RedirectToAction("Index", "Home");
            }
            HttpContext.Session.SetInt32("userId", userInDb.UserId);
            return RedirectToAction("Dashboard");
        }
        return RedirectToAction("Index", "Home");
    }

    [HttpGet("dashboard")]
    public IActionResult Dashboard()
    {
        User? loggedinUser = db.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("userId"));

        if (loggedinUser != null)
        {
            ViewBag.User = loggedinUser;
            List<Wedding> allWeddings = db.Weddings
            .Include(w => w.Creator)
            .Include(w => w.GuestList)
            .ToList();
            return View("Dashboard", allWeddings);
        }
        return RedirectToAction("Index", "Home");
    }

    [HttpPost("delete/{weddingId}")]
    public IActionResult Delete(int weddingId)
    {
        User? loggedInUser = db.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("userId"));

        if(loggedInUser == null)
        {
            return RedirectToAction("Index", "Home");
        }

        Wedding? wedding = db.Weddings.FirstOrDefault(w => w.WeddingId == weddingId);
        if(wedding != null)
        {
            db.Weddings.Remove(wedding);
            db.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        else
        {
            return RedirectToAction("Dashboard");
        }
    }

    [HttpPost("rsvp/{weddingId}")]
    public IActionResult RSVP(int weddingId)
    {
        User? loggedInUser = db.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("userId"));

        if(loggedInUser == null)
        {
            return RedirectToAction("Index", "Home");
        }

        Attendee? existingrsvp = db.Attendees.FirstOrDefault(r => r.WeddingId == weddingId && r.UserId == (int)loggedInUser.UserId);

        if (existingrsvp == null)
        {
            Attendee newrsvp = new Attendee()
            {
                UserId = (int)loggedInUser.UserId,
                WeddingId = weddingId
            };
            db.Attendees.Add(newrsvp);
        }
        else
        {
            db.Attendees.Remove(existingrsvp);
        }

        db.SaveChanges();
        return RedirectToAction("Dashboard");
    }    
}