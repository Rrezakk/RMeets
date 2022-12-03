using System.ComponentModel.DataAnnotations;

namespace RMeets.Models;

public class Target
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    public int TimesUsed { get; set; }
    public virtual ICollection<Blank>? Blanks { get; set; }
}
