using AnimalModels.Entitites;
using Microsoft.EntityFrameworkCore;

namespace AnimalModels.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
    public DbSet<Animal> Animals { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Animal>(entity =>
        {
            entity.Property(animal => animal.Color)
                .HasConversion<string>()
                .HasMaxLength(30);

            entity.Property(animal => animal.Gender)
                .HasConversion<string>()
                .HasMaxLength(30);
        });
    }
}