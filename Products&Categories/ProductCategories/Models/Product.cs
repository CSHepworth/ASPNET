#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCategories.Models;

public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required]
    public string ProductName { get; set; }

    [Required]
    public string ProductDescription { get; set; }

    [Required]
    public decimal ProductPrice { get; set; }

    public List<ProductCategory> ProductCategory { get; set; } = new List<ProductCategory>();

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}