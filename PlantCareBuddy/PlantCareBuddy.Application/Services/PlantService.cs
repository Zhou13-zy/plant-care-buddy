using PlantCareBuddy.Application.DTOs.Plant;
using PlantCareBuddy.Application.Interfaces;
using PlantCareBuddy.Domain.Entities;
using PlantCareBuddy.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using PlantCareBuddy.Domain.Enums;
using PlantCareBuddy.Infrastructure.Extensions;

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
            var plants = await _context.Plants.ToListAsync();
            return plants.Select(MapToDto);
        }
        public async Task<PlantDto?> GetPlantByIdAsync(int id)
        {
            var plant = await _context.Plants.FindAsync(id);
            if (plant == null) return null;

            return MapToDto(plant);
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

            return MapToDto(plant);
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

            return plants.Select(MapToDto);
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
            if (dto.NextHealthCheckDate.HasValue) plant.NextHealthCheckDate = dto.NextHealthCheckDate.Value;
            if (dto.Notes != null) plant.Notes = dto.Notes;
            if (dto.PrimaryImagePath != null) plant.PrimaryImagePath = dto.PrimaryImagePath;

            await _context.SaveChangesAsync();

            return MapToDto(plant);
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

            query = query
                .WhereNameContainsInsensitive(name)
                .WhereSpeciesContainsInsensitive(species)
                .WhereLocationContainsInsensitive(location)
                .WithHealthStatus(healthStatus);

            var plants = await query.ToListAsync();
            return plants.Select(MapToDto);
        }
        private static PlantDto MapToDto(Plant plant)
        {
            return new PlantDto
            {
                Id = plant.Id,
                Name = plant.Name,
                Species = plant.Species,
                Location = plant.Location,
                AcquisitionDate = plant.AcquisitionDate,
                HealthStatus = plant.HealthStatus,
                NextHealthCheckDate = plant.NextHealthCheckDate,
                Notes = plant.Notes,
                PrimaryImagePath = plant.PrimaryImagePath
            };
        }
    }
}