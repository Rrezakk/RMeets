using RMeets.Enums;
using System.ComponentModel.DataAnnotations;

namespace RMeets.Models;

public class Reaction
{
    [Key]
    public int Id { get; set; }
    public virtual Blank From { get; set; }
    public virtual Blank To { get; set; }
    public DateTime Timestamp1 { get; set; }
    public DateTime Timestamp2 { get; set; }
    public ReactionTypes ReactionType1 { get; set; }
    public ReactionTypes ReactionType2 { get; set; }
}
