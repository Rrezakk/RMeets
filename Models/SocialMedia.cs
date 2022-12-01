using System.ComponentModel.DataAnnotations;

namespace RMeets.Models;

public class SocialMedia
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(80)]
    public string Name { get; set; }
    [Required]
    [MaxLength(250)]
    public string Description { get; set; }
    [Required]
    [MaxLength(200)]
    public string BaseLink { get; set; }
}
