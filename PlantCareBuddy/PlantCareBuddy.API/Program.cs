// This is the main entry point and configuration file
using Microsoft.EntityFrameworkCore;
using PlantCareBuddy.Application.Interfaces;
using PlantCareBuddy.Application.Services;
using PlantCareBuddy.Infrastructure.Persistence;
using PlantCareBuddy.Infrastructure.Extensions;
using PlantCareBuddy.Domain.Strategies;
using PlantCareBuddy.Application.Strategies;
using PlantCareBuddy.Domain.Interfaces;
using PlantCareBuddy.Infrastructure.Services;
using PlantCareBuddy.Application.Interfaces;
using PlantCareBuddy.Application.Services;
using PlantCareBuddy.Domain.Strategies.Interfacces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Use Sql Server
builder.Services.AddDbContext<PlantCareBuddyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPlantService, PlantService>();
builder.Services.AddScoped<ICareEventService, CareEventService>();
builder.Services.AddScoped<IHealthObservationService, HealthObservationService>();

builder.Services.AddPhotoStorage(builder.Configuration);

builder.Services.AddScoped<IPlantService, PlantService>();
builder.Services.AddScoped<ICareEventService, CareEventService>();
builder.Services.AddScoped<IHealthObservationService, HealthObservationService>();

// Register strategies
builder.Services.AddScoped<ISeasonService, SeasonService>();
builder.Services.AddScoped<ICareStrategy>(sp => new DefaultCareStrategy());
builder.Services.AddScoped<ICareStrategy>(sp => new SucculentCareStrategy());
builder.Services.AddScoped<ICareStrategy>(sp => new TropicalCareStrategy());

builder.Services.AddScoped<ICareStrategyService, CareStrategyService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowFrontend");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

var imagePath = Path.Combine(builder.Environment.ContentRootPath, "wwwroot/images/plants");
if (!Directory.Exists(imagePath))
{
    Directory.CreateDirectory(imagePath);
}

app.MapControllers();

app.Run();
