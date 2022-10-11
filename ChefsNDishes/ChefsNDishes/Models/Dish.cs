#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefsNDishes.Models;

public class Dish
{
    [Key]
    public int DishId { get; set; }

    [Required]
    public string DishName { get; set; }

    [Required]
    public int DishCalories { get; set; }

    [Required]
    public int ChefId { get; set; }
    public Chef Creator { get; set; }

    [Required]
    public int DishTastiness { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}