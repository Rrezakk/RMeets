using System.ComponentModel.DataAnnotations;

namespace RMeets.Models;

public class Fact
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    public virtual ICollection<Blank> Blanks { get; set; }
}
