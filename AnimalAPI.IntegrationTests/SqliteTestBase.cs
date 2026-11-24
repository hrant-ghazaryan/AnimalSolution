using AnimalModels.Data;
using AnimalModels.Entitites;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace AnimalAPI.IntegrationTests;

public class SqliteTestBase : IDisposable
{
    private readonly SqliteConnection connection;
    private readonly DbContextOptions<AppDbContext> options;

    protected SqliteTestBase()
    {
        connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(connection)
            .Options;

        using var context = CreateContext();
        context.Database.EnsureCreated();
    }

    protected SqliteTestDbContext CreateContext()
        => new(options);

    protected async Task<Animal[]> SeedAsync(params Animal[] animals)
    {
        using var context = CreateContext();
        context.Animals.AddRange(animals);
        await context.SaveChangesAsync();
        return animals;
    }

    public void Dispose()
        => connection.Dispose();
}
