using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LoginRegister.Models;
using Microsoft.AspNetCore.Identity;

namespace LoginRegister.Controllers;

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
        return View();
    }

    [HttpPost("register")]
    public IActionResult Register(User user)
    {
        if(ModelState.IsValid)
        {
            if(dbcontext.Users.Any(user => user.Email == user.Email))
            {
                ModelState.AddModelError("Email", "Email already in use");
            }

            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            user.Password = Hasher.HashPassword(user, user.Password);

            var newUser = dbcontext.Users.Add(user);
            dbcontext.SaveChanges();
        }
        return RedirectToAction("Success");
    }

    [HttpPost("login")]
    public IActionResult Login(LoginUser userSubmission)
    {
        if(ModelState.IsValid)
        {
            var userinDb = dbcontext.Users.FirstOrDefault(user => user.Email == userSubmission.LoginEmail);

            if (userinDb == null)
            {
                ModelState.AddModelError("Email", "Invalid Email/Password");
                return View("Index");
            }

            var hasher = new PasswordHasher<LoginUser>();

            var result = hasher.VerifyHashedPassword(userSubmission, userinDb.Password, userSubmission.LoginPassword);

            if(result == 0)
            {
                ModelState.AddModelError("Password", "Invalid Password");
                return View("Index");
            }
        }
        return RedirectToAction("Success");
    }

    [HttpGet("success")]
    public IActionResult Success()
    {
        return View("Success");
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
