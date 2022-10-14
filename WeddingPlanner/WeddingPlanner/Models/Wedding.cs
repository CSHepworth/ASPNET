#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models;

public class Wedding
{
    [Key]
    public int WeddingId { get; set; }

    [Required]
    public string Wedder1 { get; set; }

    [Required]
    public string Wedder2 { get; set; }

    [Required]
    public DateTime WeddingDate { get; set; }

    [Required]
    public string WeddingAddress { get; set; }

    public int UserId { get; set; }
    public User? Creator { get; set; }

    public List<Attendee> GuestList { get; set; } = new List<Attendee>();

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}