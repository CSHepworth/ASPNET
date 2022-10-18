#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawnWizardApp.Models;

public class Employee
{
    [Key]
    public int EmployeeId { get; set; }

    [Display(Name = "First Name")]
    [Required(ErrorMessage = "is required")]
    [StringLength(45)]
    [MinLength(2, ErrorMessage = "First Name must be at least 2 characters.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "is required")]
    [StringLength(45)]
    [MinLength(2, ErrorMessage = "Last Name must be at least 2 characters.")]
    public string LastName { get; set; }

    [DataType(DataType.PhoneNumber)]
    [Required(ErrorMessage = "is required")]
    [StringLength(10)]
    [MinLength(10, ErrorMessage = "Please Enter a valid phone number.")]
    public string Phone { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "is required")]
    public DateTime DoB { get; set; }

    public int? AdminStatus { get; set; } = 0;
    
    [EmailAddress]
    [Required(ErrorMessage = "is required")]
    [StringLength(129)]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "is required")]
    [StringLength(100)]
    public string Password { get; set; }

    [NotMapped]
    [Compare("Password")]
    [StringLength(100)]
    [MinLength(4, ErrorMessage = "Password must be at least 4 characters")]
    [DataType(DataType.Password)]
    public string Confirm { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
}