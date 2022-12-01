using System.ComponentModel.DataAnnotations;

namespace RMeets.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Login { get; set; }
    [Required]
    [MaxLength(100)]
    public string PasswordHash { get; set; }
    public virtual Profile Profile { get; set; }
    public User()
    {
        
    }
    public User(string login, string passwordHash)
    {
        Login = login;
        PasswordHash = passwordHash;
    }
}
