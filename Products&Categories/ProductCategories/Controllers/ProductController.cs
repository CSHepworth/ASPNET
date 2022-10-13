using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProductCategories.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace ProductCategories.Controllers;

public class ProductController : Controller
{
    private MyContext dbcontext;

    public ProductController(MyContext context)
    {
        dbcontext = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        List<Product> products = dbcontext.Products.ToList();
        return View("Index", products);
    }

    [HttpPost("createproduct")]
    public IActionResult CreateProduct(Product product)
    {
        if(ModelState.IsValid)
        {
            if(dbcontext.Products.Any(p => p.ProductName == product.ProductName))
            {
                ModelState.AddModelError("ProductName", "Product Name already exists.");
            }

            var newProduct = dbcontext.Add(product);
            dbcontext.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    [HttpGet("products/{productId}/view")]
    public IActionResult ViewProduct(int productId)
    {
        ViewBag.categories = dbcontext.Categories.ToList();
        var prodcats = dbcontext.Products
            .Include(p => p.ProductCategory)
                .ThenInclude(pc => pc.Category)
            .FirstOrDefault(p => p.ProductId == productId);

        if(prodcats == null)
        {
            return RedirectToAction("Index");
        }
        return View("ViewProduct", prodcats);
    }

    [HttpPost("AddCategory/{productId}/{categoryId}")]
    public IActionResult AddCategoryToProduct(ProductCategory productcategory)
    {
        var newProductCategory = dbcontext.Add(productcategory);
        return RedirectToAction("Index");
    }

}