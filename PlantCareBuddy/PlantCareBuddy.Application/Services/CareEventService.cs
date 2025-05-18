using Microsoft.EntityFrameworkCore;
using PlantCareBuddy.Application.DTOs.CareEvent;
using PlantCareBuddy.Application.Interfaces;
using PlantCareBuddy.Domain.Entities;
using PlantCareBuddy.Infrastructure.Persistence;
using PlantCareBuddy.Infrastructure.Interfaces.Storage;
using static System.String;

namespace PlantCareBuddy.Application.Services
{
    public class CareEventService : ICareEventService
    {
        private readonly PlantCareBuddyContext _context;
        private readonly IPhotoStorageService _photoStorage;

        public CareEventService(PlantCareBuddyContext context, IPhotoStorageService photoStorage)
        {
            _context = context;
            _photoStorage = photoStorage;
        }

        public async Task<IEnumerable<CareEventDto>> GetAllCareEventsAsync()
        {
            var careEvents = await _context.CareEvents
                .Include(ce => ce.Plant)
                .OrderByDescending(ce => ce.EventDate)
                .ToListAsync();

            return careEvents.Select(MapToDto);
        }

        public async Task<IEnumerable<CareEventDto>> GetCareEventsByPlantIdAsync(int plantId)
        {
            var careEvents = await _context.CareEvents
                .Include(ce => ce.Plant)
                .Where(ce => ce.PlantId == plantId)
                .OrderByDescending(ce => ce.EventDate)
                .ToListAsync();

            return careEvents.Select(MapToDto);
        }

        public async Task<CareEventDto?> GetCareEventByIdAsync(int id)
        {
            var careEvent = await _context.CareEvents
                .Include(ce => ce.Plant)
                .FirstOrDefaultAsync(ce => ce.Id == id);

            if (careEvent == null)
                return null;

            return MapToDto(careEvent);
        }

        public async Task<CareEventDto> CreateCareEventAsync(CreateCareEventDto dto, IPhotoStorageService photoStorage)
        {
            // Verify that the plant exists
            var plant = await _context.Plants.FindAsync(dto.PlantId);
            if (plant == null)
                throw new ArgumentException($"Plant with ID {dto.PlantId} not found");

            string? beforeImagePath = null;
            string? afterImagePath = null;

            // Process before photo
            if (dto.BeforePhoto != null)
                beforeImagePath = await photoStorage.StorePhotoAsync(dto.BeforePhoto, "care-events");

            // Process after photo
            if (dto.AfterPhoto != null)
                afterImagePath = await photoStorage.StorePhotoAsync(dto.AfterPhoto, "care-events");

            var careEvent = new CareEvent
            {
                PlantId = dto.PlantId,
                EventType = dto.EventType,
                EventDate = dto.EventDate,
                Notes = dto.Notes,
                BeforeImagePath = beforeImagePath,
                AfterImagePath = afterImagePath
            };

            _context.CareEvents.Add(careEvent);
            await _context.SaveChangesAsync();

            // Reload the care event with plant data for the response
            await _context.Entry(careEvent).Reference(ce => ce.Plant).LoadAsync();

            return MapToDto(careEvent);
        }
        public async Task<CareEventDto?> UpdateCareEventAsync(int id, UpdateCareEventDto dto, IPhotoStorageService photoStorage)
        {
            var careEvent = await _context.CareEvents
                .Include(ce => ce.Plant)
                .FirstOrDefaultAsync(ce => ce.Id == id);

            if (careEvent == null)
                return null;

            careEvent.EventType = dto.EventType;
            careEvent.EventDate = dto.EventDate;
            careEvent.Notes = dto.Notes;

            // Handle Before Photo
            if (dto.BeforePhoto != null)
            {
                // Delete existing photo if there is one
                if (!IsNullOrEmpty(careEvent.BeforeImagePath))
                    await photoStorage.DeletePhotoAsync(careEvent.BeforeImagePath);

                // Set new photo path or null
                careEvent.BeforeImagePath = dto.BeforePhoto != null
                    ? await photoStorage.StorePhotoAsync(dto.BeforePhoto, "care-events")
                    : null;
            }

            // Handle After Photo
            if (dto.AfterPhoto != null)
            {
                // Delete existing photo if there is one
                if (!IsNullOrEmpty(careEvent.AfterImagePath))
                    await photoStorage.DeletePhotoAsync(careEvent.AfterImagePath);

                // Set new photo path or null
                careEvent.AfterImagePath = dto.AfterPhoto != null
                    ? await photoStorage.StorePhotoAsync(dto.AfterPhoto, "care-events")
                    : null;
            }

            await _context.SaveChangesAsync();

            return MapToDto(careEvent);
        }
        public async Task<bool> DeleteCareEventAsync(int id)
        {
            var careEvent = await _context.CareEvents.FindAsync(id);
            if (careEvent == null) return false;

            _context.CareEvents.Remove(careEvent);
            await _context.SaveChangesAsync();
            return true;
        }

        private static CareEventDto MapToDto(CareEvent careEvent)
        {
            return new CareEventDto
            {
                Id = careEvent.Id,
                PlantId = careEvent.PlantId,
                PlantName = careEvent.Plant?.Name ?? "Unknown Plant",
                EventType = careEvent.EventType,
                EventTypeName = careEvent.EventType.ToString(),
                EventDate = careEvent.EventDate,
                Notes = careEvent.Notes,
                BeforeImagePath = careEvent.BeforeImagePath,
                AfterImagePath = careEvent.AfterImagePath
            };
        }
    }
}