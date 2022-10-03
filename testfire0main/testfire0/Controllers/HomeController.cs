using Microsoft.AspNetCore.Mvc;
namespace testfire0.Controllers;
    public class HomeController : Controller
    {
        [HttpGet("")]
        public ViewResult Index()
        {
            return View("Index");
        }

        [HttpGet("projects")]
        public IActionResult Projects()
        {
            return View("Projects");
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View("Contact");
        }
    }