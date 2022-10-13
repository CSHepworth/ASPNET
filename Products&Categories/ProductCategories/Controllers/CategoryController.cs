using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProductCategories.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace ProductCategories.Controllers;

public class CategoryController : Controller
{
    private MyContext dbcontext;

    public CategoryController(MyContext context)
    {
        dbcontext = context;
    }

    [HttpGet("categories")]
    public IActionResult Categories()
    {
        List<Category> categories = dbcontext.Categories.ToList();
        return View(categories);
    }

    [HttpPost("createcategory")]
    public IActionResult CreateCategory(Category category)
    {
        if(ModelState.IsValid)
        {
            if(dbcontext.Categories.Any(c => c.CategoryName == category.CategoryName))
            {
                ModelState.AddModelError("CategoryName", "Category Name already exists.");
            }

            var newCategory = dbcontext.Add(category);
            dbcontext.SaveChanges();
        }
        return RedirectToAction("Categories");
    }
}