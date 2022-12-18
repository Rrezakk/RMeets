using Microsoft.EntityFrameworkCore;
using RMeets.Models;
using RMeets.Staff;

namespace RMeets.Contexts
{
    public sealed class ApplicationContext : DbContext
    {
        public DbSet<City> CitySet { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Blank> Blanks { get; set; } = null!;
        public DbSet<BlankPhoto> BlankPhotos { get; set; } = null!;
        public DbSet<Fact> Facts { get; set; } = null!;
        public DbSet<Gender> Genders { get; set; } = null!;
        public DbSet<Interest> Interests { get; set; } = null!;
        public DbSet<Profile> Profiles { get; set; } = null!;
        public DbSet<Reaction> Reactions { get; set; } = null!;
        public DbSet<Target> Targets { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(a => a.Profile)
                .WithOne(b => b.User)
                .HasForeignKey<Profile>(b => b.UserRef);
            modelBuilder.Entity<City>()
                .HasMany(a => a.Profiles)
                .WithOne(b => b.City).HasForeignKey(x=>x.CityId);


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