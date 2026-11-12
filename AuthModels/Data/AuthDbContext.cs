using AuthModels.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthModels.Data;

public class AuthDbContext : DbContext
{
    public DbSet<ApplicationUser> Users { get; set; }
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(e => e.UserName).IsRequired();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.Password).IsRequired();
        });

        modelBuilder.Entity<ApplicationUser>().
            HasIndex(u => u.Email).
            IsUnique();

        modelBuilder.Entity<ApplicationUser>().
            HasIndex(u => u.UserName).
            IsUnique();
    }
}
