using Microsoft.EntityFrameworkCore;
using PlantCareBuddy.Application.DTOs.HealthObservation;
using PlantCareBuddy.Application.Interfaces;
using PlantCareBuddy.Domain.Entities;
using PlantCareBuddy.Infrastructure.Persistence;
using PlantCareBuddy.Infrastructure.Interfaces.Storage;
using static System.String;

namespace PlantCareBuddy.Application.Services
{
    public class HealthObservationService : IHealthObservationService
    {
        private readonly PlantCareBuddyContext _context;
        private readonly IPhotoStorageService _photoStorage;

        public HealthObservationService(PlantCareBuddyContext context, IPhotoStorageService photoStorage)
        {
            _context = context;
            _photoStorage = photoStorage;
        }

        public async Task<IEnumerable<HealthObservationDto>> GetAllHealthObservationsAsync()
        {
            var observations = await _context.HealthObservations
                .Include(ho => ho.Plant)
                .OrderByDescending(ho => ho.ObservationDate)
                .ToListAsync();

            return observations.Select(MapToDto);
        }

        public async Task<IEnumerable<HealthObservationDto>> GetHealthObservationsByPlantIdAsync(int plantId)
        {
            var observations = await _context.HealthObservations
                .Include(ho => ho.Plant)
                .Where(ho => ho.PlantId == plantId)
                .OrderByDescending(ho => ho.ObservationDate)
                .ToListAsync();

            return observations.Select(MapToDto);
        }

        public async Task<HealthObservationDto?> GetHealthObservationByIdAsync(int id)
        {
            var observation = await _context.HealthObservations
                .Include(ho => ho.Plant)
                .FirstOrDefaultAsync(ho => ho.Id == id);

            if (observation == null)
                return null;

            return MapToDto(observation);
        }

        public async Task<HealthObservationDto> CreateHealthObservationAsync(CreateHealthObservationDto dto, IPhotoStorageService photoStorage)
        {
            // Verify that the plant exists
            var plant = await _context.Plants.FindAsync(dto.PlantId);
            if (plant == null)
                throw new ArgumentException($"Plant with ID {dto.PlantId} not found");

            string? imagePath = null;
            if (dto.Photo != null)
                imagePath = await photoStorage.StorePhotoAsync(dto.Photo, "health-observations");

            var observation = new HealthObservation
            {
                PlantId = dto.PlantId,
                ObservationDate = dto.ObservationDate,
                HealthStatus = dto.HealthStatus,
                Notes = dto.Notes,
                ImagePath = imagePath
            };

            _context.HealthObservations.Add(observation);
            await _context.SaveChangesAsync();

            // Find the latest observation for this plant by date, which might be the one we just created
            // or could be a previously existing observation with a later date
            var latestObservation = await _context.HealthObservations
                .Where(ho => ho.PlantId == dto.PlantId)
                .OrderByDescending(ho => ho.ObservationDate)
                .FirstOrDefaultAsync();

            // Update the plant's health status only if our new observation is the latest
            if (latestObservation != null && latestObservation.Id == observation.Id)
            {
                plant.HealthStatus = dto.HealthStatus;
                await _context.SaveChangesAsync();
            }

            // Reload the observation with plant data for the response
            await _context.Entry(observation).Reference(ho => ho.Plant).LoadAsync();

            return MapToDto(observation);
        }

        public async Task<HealthObservationDto?> UpdateHealthObservationAsync(int id, UpdateHealthObservationDto dto, IPhotoStorageService photoStorage)
        {
            var observation = await _context.HealthObservations
                .Include(ho => ho.Plant)
                .FirstOrDefaultAsync(ho => ho.Id == id);

            if (observation == null)
                return null;

            observation.HealthStatus = dto.HealthStatus;
            observation.ObservationDate = dto.ObservationDate;
            observation.Notes = dto.Notes;

            if (dto.Photo != null)
            {
                // Delete existing photo if there is one
                if (!IsNullOrEmpty(observation.ImagePath))
                    await photoStorage.DeletePhotoAsync(observation.ImagePath);

                // Set new photo path or null
                observation.ImagePath = dto.Photo != null
                    ? await photoStorage.StorePhotoAsync(dto.Photo, "health-observations")
                    : null;
            }

            await _context.SaveChangesAsync();

            // Update the plant's health status if this is the latest observation
            var latestObservation = await _context.HealthObservations
                .Where(ho => ho.PlantId == observation.PlantId)
                .OrderByDescending(ho => ho.ObservationDate)
                .FirstOrDefaultAsync();

            if (latestObservation != null && latestObservation.Id == observation.Id)
            {
                var plant = await _context.Plants.FindAsync(observation.PlantId);
                if (plant != null)
                {
                    plant.HealthStatus = observation.HealthStatus;
                    await _context.SaveChangesAsync();
                }
            }

            return MapToDto(observation);
        }

        public async Task<bool> DeleteHealthObservationAsync(int id)
        {
            var observation = await _context.HealthObservations.FindAsync(id);
            if (observation == null) return false;

            _context.HealthObservations.Remove(observation);
            await _context.SaveChangesAsync();

            // Update plant health status to the most recent observation if any exist
            var plantId = observation.PlantId;
            var latestObservation = await _context.HealthObservations
                .Where(ho => ho.PlantId == plantId)
                .OrderByDescending(ho => ho.ObservationDate)
                .FirstOrDefaultAsync();

            if (latestObservation != null)
            {
                var plant = await _context.Plants.FindAsync(plantId);
                if (plant != null)
                {
                    plant.HealthStatus = latestObservation.HealthStatus;
                    await _context.SaveChangesAsync();
                }
            }

            return true;
        }

        private static HealthObservationDto MapToDto(HealthObservation observation)
        {
            return new HealthObservationDto
            {
                Id = observation.Id,
                PlantId = observation.PlantId,
                PlantName = observation.Plant?.Name ?? "Unknown Plant",
                ObservationDate = observation.ObservationDate,
                HealthStatus = observation.HealthStatus,
                HealthStatusName = observation.HealthStatus.ToString(),
                Notes = observation.Notes,
                ImagePath = observation.ImagePath
            };
        }
    }
}