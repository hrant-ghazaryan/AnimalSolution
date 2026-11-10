using Microsoft.EntityFrameworkCore;
using TranslationModels.Data;
using TranslationServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.
    GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<TranslationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<ITranslationService, TranslationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
