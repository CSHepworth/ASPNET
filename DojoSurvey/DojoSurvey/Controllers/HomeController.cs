using Microsoft.AspNetCore.Mvc;
namespace DojoSurvey.Controllers;
    public class HomeController : Controller
    {
        [HttpGet("")]
        public ViewResult Index()
        {
            return View("Index");
        }

        [HttpPost("result")]
        public IActionResult Result(string name, string location, string language, string comment)
        {
            ViewBag.Name = name;
            ViewBag.Location = location;
            ViewBag.Language = language;
            ViewBag.Comment = comment;
            return View("Result");
        }

    }