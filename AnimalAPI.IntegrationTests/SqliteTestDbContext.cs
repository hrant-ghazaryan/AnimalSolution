using AnimalModels.Data;
using AnimalModels.Entitites;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace AnimalAPI.IntegrationTests;

public class SqliteTestDbContext : AppDbContext
{
    public SqliteTestDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Animal>()
            .Property(a => a.RowVersion)
            .ValueGeneratedNever();
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        SetNewRowVersions();

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void SetNewRowVersions()
    {
        foreach (var entry in ChangeTracker.Entries<Animal>().
            Where(e => e.State == EntityState.Added ||
                       e.State == EntityState.Modified))
            entry.Entity.RowVersion = RandomNumberGenerator.GetBytes(8);
    }
}
