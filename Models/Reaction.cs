using System.ComponentModel.DataAnnotations;

namespace RMeets.Models;

public class Reaction
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Value { get; set; }
    public Blank From { get; set; }
    public Blank To { get; set; }
    public DateTime Timestamp1 { get; set; }
    public DateTime Timestamp2 { get; set; }
    public ReactionType ReactionType1 { get; set; }
    public ReactionType ReactionType2 { get; set; }
}
