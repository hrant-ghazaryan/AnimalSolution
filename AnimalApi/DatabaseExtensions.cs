using AnimalModels.Data;
using Microsoft.EntityFrameworkCore;

namespace AnimalApi;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, 
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException("DefaultConnection string is not configured.");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString);

            if (environment.IsDevelopment())
            {
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            }
        });

        return services;
    }
}
