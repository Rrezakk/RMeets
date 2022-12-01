using RMeets.Enums;
using System.ComponentModel.DataAnnotations;

namespace RMeets.Models;

public class AccessibilityLevel
{
    [Key]
    public int Id { get; set; }
    [Required]
    public AccessibilityLevels Level { get; set; }
    [Required]
    [MaxLength(100)]
    public string Value { get; set; }
}

