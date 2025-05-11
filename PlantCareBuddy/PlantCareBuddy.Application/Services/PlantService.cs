using PlantCareBuddy.Application.DTOs.Plant;
using PlantCareBuddy.Application.Interfaces;
using PlantCareBuddy.Domain.Entities;
using PlantCareBuddy.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace PlantCareBuddy.Application.Services
{
    public class PlantService : IPlantService
    {
        private readonly PlantCareBuddyContext _context;

        public PlantService(PlantCareBuddyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PlantDto>> GetAllPlantsAsync()
        {
            return await _context.Plants
                .Select(p => new PlantDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Species = p.Species,
                    Location = p.Location,
                    AcquisitionDate = p.AcquisitionDate,
                    HealthStatus = p.HealthStatus.ToString(),
                    Notes = p.Notes,
                    PrimaryImagePath = p.PrimaryImagePath
                })
                .ToListAsync();
        }
        public async Task<PlantDto?> GetPlantByIdAsync(int id)
        {
            var plant = await _context.Plants.FindAsync(id);
            if (plant == null) return null;

            return new PlantDto
            {
                Id = plant.Id,
                Name = plant.Name,
                Species = plant.Species,
                Location = plant.Location,
                AcquisitionDate = plant.AcquisitionDate,
                HealthStatus = plant.HealthStatus.ToString(),
                Notes = plant.Notes,
                PrimaryImagePath = plant.PrimaryImagePath
            };
        }
        public async Task<PlantDto> CreatePlantAsync(CreatePlantDto dto)
        {
            var plant = new Plant
            {
                Name = dto.Name,
                Species = dto.Species,
                AcquisitionDate = dto.AcquisitionDate,
                Location = dto.Location,
                HealthStatus = dto.HealthStatus,
                Notes = dto.Notes,
                PrimaryImagePath = dto.PrimaryImagePath
            };

            _context.Plants.Add(plant);
            await _context.SaveChangesAsync();

            return new PlantDto
            {
                Id = plant.Id,
            };
        }
        public async Task<IEnumerable<PlantDto>> CreatePlantsAsync(List<CreatePlantDto> dtos)
        {
            var plants = dtos.Select(dto => new Plant
            {
                Name = dto.Name,
                Species = dto.Species,
                AcquisitionDate = dto.AcquisitionDate,
                Location = dto.Location,
                HealthStatus = dto.HealthStatus,
                Notes = dto.Notes,
                PrimaryImagePath = dto.PrimaryImagePath
            }).ToList();

            _context.Plants.AddRange(plants);
            await _context.SaveChangesAsync();

            return plants.Select(plant => new PlantDto
            {
                Id = plant.Id,
            }).ToList();
        }
    }
}