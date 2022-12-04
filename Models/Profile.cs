using System.ComponentModel.DataAnnotations;

namespace RMeets.Models;

public class Profile
{
    [Key]
    public int Id { get; set; }
    [Required]
    public virtual User User { get; set; }
    [Required]
    public int UserRef { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    public virtual Gender Gender { get; set; }
    [Required]
    public int Age { get; set; }

    public int? ChosenBlankId { get; set; } = null;//can be null
    [Required]
    public int? CityId { get; set; }
    [Required]
    public virtual City? City { get; set; }
    public string SocialMediaLink { get; set; }
    [Required]
    public DateTime RegistrationTimestamp { get; set; }
    public virtual Role? Role { get; set; }//Nullable
    public virtual ICollection<Blank>? Blanks { get; set; }
}
