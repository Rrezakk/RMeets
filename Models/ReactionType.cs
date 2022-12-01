using System.ComponentModel.DataAnnotations;

namespace RMeets.Models;

public class ReactionType
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int Key { get; set; }
    [Required]
    [MaxLength(50)]
    public string Value { get; set; }
}
