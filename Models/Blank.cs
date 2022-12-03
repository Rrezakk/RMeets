using System.ComponentModel.DataAnnotations;

namespace RMeets.Models;

public class Blank
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(300)]
    public string Description { get; set; }
    [Required]
    public int CurrentGenderId { get; set; }
    [Required]
    public virtual Gender? CurrentGender { get; set; }
    [Required]
    public virtual Target? Target { get; set; }
    [Required]
    public int TargetId { get; set; }
    [Required]
    public virtual Profile? Profile { get; set; }
    [Required]
    public int ProfileId { get; set; }
    [Required]
    public virtual ICollection<BlankPhoto> Photos { get; set; }
    [Required]
    public virtual ICollection<Interest> Interests { get; set; }
    [Required]
    public virtual ICollection<Fact> Facts { get; set; }
    public bool Moderated { get; set; } = false;
    public Blank()
    {
    }
}
