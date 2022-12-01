using System.ComponentModel.DataAnnotations;

namespace RMeets.Models;

public class Role
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    [MaxLength(200)]
    public string Description { get; set; }
}
