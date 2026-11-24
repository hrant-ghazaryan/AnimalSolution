using AnimalModels.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace AnimalAPI.IntegrationTests;

public class AnimalServiceTests : IDisposable
{
    private readonly SqliteConnection connection;
    private readonly DbContextOptions<AppDbContext> options;

    public AnimalServiceTests()
    {
        connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();


    }
    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
