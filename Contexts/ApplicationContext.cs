using Microsoft.EntityFrameworkCore;
using RMeets.Models;
using RMeets.Staff;

namespace RMeets.Contexts
{
    public sealed class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<AccessibilityLevel> AccessibilityLevels { get; set; }
        public DbSet<Blank> Blanks { get; set; }
        public DbSet<BlankPhoto> BlankPhotos { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Fact> Facts { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Target> Targets { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(a => a.Profile)
                .WithOne(b => b.User)
                .HasForeignKey<Profile>(b => b.UserRef);
               
                
            modelBuilder.Entity<Blank>().HasOne(p => p.Target)
                .WithMany(t => t.Blanks)
                .HasForeignKey(p => p.TargetId);
            modelBuilder.Entity<Blank>().HasOne(p => p.CurrentGender)
                .WithMany(t => t.Blanks)
                .HasForeignKey(p => p.CurrentGenderId);
            modelBuilder.Entity<Blank>().HasOne(p => p.Profile)
                .WithMany(t => t.Blanks)
                .HasForeignKey(p => p.ProfileId);
            modelBuilder.Entity<BlankPhoto>().HasOne(p => p.Blank)
                .WithMany(t => t.Photos)
                .HasForeignKey(p => p.BlankId);
           
            
            modelBuilder.ApplyUtcDateTimeConverter();//Put before seed data and after model creation
        }
    }
}