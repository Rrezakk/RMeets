using RMeets.Enums;
using System.ComponentModel.DataAnnotations;

namespace RMeets.Models;

public class BlankPhoto
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(500)]
    public string Url { get; set; }
    [Required]
    public int BlankId { get; set; }
    [Required]
    public virtual Blank Blank { get; set; }
    [Required]
    public virtual AccessibilityLevel AccessibilityLevel { get; set; }
}
