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
    public Gender Gender { get; set; }
    [Required]
    public int Age { get; set; }
    [Required]
    public City City { get; set; }
    [Required]
    public DateTime RegistrationTimestamp { get; set; }
    //Nullable
    public Role Role { get; set; }
    public Profile()
    {
        
    }
    public Profile(int userRef,
        string name, Gender gender, int age, City city,
        DateTime registrationTimestamp, Role role)
    {
        UserRef = userRef;
        Name = name;
        Gender = gender;
        Age = age;
        City = city;
        RegistrationTimestamp = registrationTimestamp;
        Role = role;
    }
    public ICollection<MediaProfileLink> SocialMediaList { get; set; }
}
