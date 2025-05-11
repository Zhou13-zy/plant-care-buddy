using PlantCareBuddy.Application.DTOs.Plant;
using PlantCareBuddy.Application.Interfaces;
using PlantCareBuddy.Domain.Entities;
using PlantCareBuddy.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using PlantCareBuddy.Domain.Enums;

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
        public async Task<PlantDto?> UpdatePlantAsync(int id, UpdatePlantDto dto)
        {
            var plant = await _context.Plants.FindAsync(id);
            if (plant == null) return null;

            if (dto.Name != null) plant.Name = dto.Name;
            if (dto.Species != null) plant.Species = dto.Species;
            if (dto.AcquisitionDate.HasValue) plant.AcquisitionDate = dto.AcquisitionDate.Value;
            if (dto.Location != null) plant.Location = dto.Location;
            if (dto.HealthStatus.HasValue) plant.HealthStatus = dto.HealthStatus.Value;
            if (dto.Notes != null) plant.Notes = dto.Notes;
            if (dto.PrimaryImagePath != null) plant.PrimaryImagePath = dto.PrimaryImagePath;

            await _context.SaveChangesAsync();

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
        public async Task<bool> DeletePlantAsync(int id)
        {
            var plant = await _context.Plants.FindAsync(id);
            if (plant == null) return false;

            _context.Plants.Remove(plant);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<PlantDto>> SearchPlantsAsync(string? name, string? species, PlantHealthStatus? healthStatus, string? location)
        {
            var query = _context.Plants.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(p => p.Name.Contains(name));
            if (!string.IsNullOrEmpty(species))
                query = query.Where(p => p.Species.Contains(species));
            if (healthStatus.HasValue)
                query = query.Where(p => p.HealthStatus == healthStatus.Value);
            if (!string.IsNullOrEmpty(location))
                query = query.Where(p => p.Location.Contains(location));

            return await query
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
    }
}