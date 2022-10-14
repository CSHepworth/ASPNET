#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "First Name must be at least 2 characters.")]
    public string FirstName { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "Last Name must be at least 2 characters.")]
    public string LastName { get; set; }

    [EmailAddress]
    [Required]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    [Required]
    [MinLength(4, ErrorMessage = "Password must be at least 4 characters.")]
    public string Password { get; set; }

    public List<Attendee> WeddingList { get; set; } = new List<Attendee>();

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    [NotMapped]
    [Compare("Password")]
    [DataType(DataType.Password)]
    public string Confirm { get; set; }
}