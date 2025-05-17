// This is the main entry point and configuration file
using Microsoft.EntityFrameworkCore;
using PlantCareBuddy.Application.Interfaces;
using PlantCareBuddy.Application.Services;
using PlantCareBuddy.Infrastructure.Persistence;
using PlantCareBuddy.Infrastructure.Extensions;

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

app.MapControllers();

app.Run();
