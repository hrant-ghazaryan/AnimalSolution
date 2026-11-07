using AnimalModels.Entitites.Abstractions;
using AnimalModels.Entitites.AnimalEntities;
using Microsoft.EntityFrameworkCore;

namespace AnimalModels.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
    public DbSet<Animal> Animals => Set<Animal>();
    public DbSet<Dog> Dogs => Set<Dog>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animal>()
        .UseTpcMappingStrategy();

        modelBuilder.Entity<Dog>().
            HasIndex(d => d.MicrochipNumber).
            IsUnique().
            HasFilter("[MicrochipNumber] IS NOT NULL");

        base.OnModelCreating(modelBuilder);
    }
}