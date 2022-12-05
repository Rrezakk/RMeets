using RMeets.Enums;
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

    [Required] public virtual UserRoles? Role { get; set; } = UserRoles.User;
    public virtual Profile? Profile { get; set; }
}
