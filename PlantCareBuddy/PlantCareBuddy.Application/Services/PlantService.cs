using PlantCareBuddy.Application.DTOs.Plant;
using PlantCareBuddy.Application.Interfaces;
using PlantCareBuddy.Domain.Entities;
using PlantCareBuddy.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using PlantCareBuddy.Domain.Enums;
using PlantCareBuddy.Infrastructure.Extensions;
using PlantCareBuddy.Infrastructure.Interfaces.Storage;
using static System.String;

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
        public async Task<PlantDto> CreatePlantAsync(CreatePlantDto dto, IPhotoStorageService photoStorage)
        {
            string? imagePath = null;
            string? imagePathHO = null;
            if (dto.Photo != null)
                imagePath = await photoStorage.StorePhotoAsync(dto.Photo, "plants");
                imagePathHO = await photoStorage.StorePhotoAsync(dto.Photo, "health-observations");

            var plant = new Plant
            {
                Name = dto.Name,
                Species = dto.Species,
                AcquisitionDate = dto.AcquisitionDate,
                Location = dto.Location,
                HealthStatus = dto.HealthStatus,
                Notes = dto.Notes,
                PrimaryImagePath = imagePath
            };

            _context.Plants.Add(plant);
            await _context.SaveChangesAsync();

            // Create initial health observation
            var initialObservation = new HealthObservation
            {
                PlantId = plant.Id,
                ObservationDate = dto.AcquisitionDate,
                HealthStatus = dto.HealthStatus,
                Notes = $"Initial health assessment upon acquiring {dto.Name}.",
                ImagePath = imagePathHO
            };
            _context.HealthObservations.Add(initialObservation);

            // Create initial care event
            var wateringEvent = new CareEvent
            {
                PlantId = plant.Id,
                EventType = CareEventType.Watering,
                EventDate = dto.AcquisitionDate,
                Notes = "Initial watering after acquisition."
            };
            _context.CareEvents.Add(wateringEvent);

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
        public async Task<PlantDto?> UpdatePlantAsync(int id, UpdatePlantDto dto, IPhotoStorageService photoStorage)
        {
            var plant = await _context.Plants.FindAsync(id);
            if (plant == null) return null;

            plant.Name = dto.Name;
            plant.Species = dto.Species;
            plant.AcquisitionDate = dto.AcquisitionDate;
            plant.Location = dto.Location;
            plant.NextHealthCheckDate = dto.NextHealthCheckDate;
            plant.Notes = dto.Notes;

            if (dto.Photo != null)
            {
                // Delete existing photo if there is one
                if (!IsNullOrEmpty(plant.PrimaryImagePath))
                    await photoStorage.DeletePhotoAsync(plant.PrimaryImagePath);

                // Set new photo path or null
                plant.PrimaryImagePath = dto.Photo != null
                    ? await photoStorage.StorePhotoAsync(dto.Photo, "plants")
                    : null;
            }

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