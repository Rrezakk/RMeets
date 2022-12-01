using System.ComponentModel.DataAnnotations;

namespace RMeets.Models;

public class MediaProfileLink
{
    [Key]
    public int Id { get; set; }
    [Required]
    public SocialMedia SocialMedia { get; set; }
    [Required]
    public Profile Profile { get; set; }
    [Required]
    [MaxLength(200)]
    public string PartialLink { get; set; }
}
