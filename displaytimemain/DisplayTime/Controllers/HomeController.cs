using Microsoft.AspNetCore.Mvc;
namespace DisplayTime.Controllers;
    public class HomeController : Controller
    {
        [HttpGet("")]
        public ViewResult Index()
        {
            ViewBag.CurrentTime = DateTime.Now;
            return View("Index");
        }
    }