using AnimalApi;
using AnimalModels.Data;
using AnimalServices.Implementations;
using AnimalServices.Intefaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IAnimalService, AnimalService>();
builder.Services.AddScoped<ExceptionHandlingMiddleware>();
var connectionString = builder.Configuration.
    GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

var translationApiBaseUrl = builder.Configuration["TranslationApi:BaseUrl"]
    ?? throw new InvalidOperationException("TranslationApi BaseUrl is not configured.");

builder.Services.AddHttpClient<ITranslationApiClient, TranslationApiClient>(httpClient =>
{
    httpClient.BaseAddress = new Uri(translationApiBaseUrl);
});

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
