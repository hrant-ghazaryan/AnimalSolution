using AnimalApi;
using AnimalApi.Middlewares;
using AnimalApi.TranslationClient;
using AnimalServices.Implementations;
using AnimalServices.Intefaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenApi();
builder.Services.AddScoped<IAnimalService, AnimalService>();
builder.Services.AddScoped<ExceptionHandlingMiddleware>();

builder.Services.AddDatabase(builder.Configuration, builder.Environment);
builder.Services.AddTranslationApi(builder.Configuration);

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
