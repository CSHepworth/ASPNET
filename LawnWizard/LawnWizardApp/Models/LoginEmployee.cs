#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawnWizardApp.Models;

public class LoginEmployee
{
    [EmailAddress]
    [Required]
    [Display(Name = "Email")]
    public string LoginEmail { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string LoginPassword { get; set; }
}