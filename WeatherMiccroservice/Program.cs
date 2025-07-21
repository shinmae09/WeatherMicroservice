using WeatherMicroservice.Database;
using WeatherMicroservice.Interfaces;
using WeatherMicroservice.Providers;
using WeatherMicroservice.Repositories;
using WeatherMicroservice.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AppDbContext>();

builder.Services.AddHttpClient<IWeatherApiProvider, OpenWeatherMapApiProvider>();
builder.Services.AddScoped<IWeatherApiProvider, OpenWeatherMapApiProvider>();

builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<IWeatherRepository, WeatherRepository>();


var app = builder.Build();

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
