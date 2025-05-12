using Microsoft.EntityFrameworkCore;
using PlantCareBuddy.Application.DTOs.CareEvent;
using PlantCareBuddy.Application.Interfaces;
using PlantCareBuddy.Domain.Entities;
using PlantCareBuddy.Infrastructure.Persistence;

namespace PlantCareBuddy.Application.Services
{
    public class CareEventService : ICareEventService
    {
        private readonly PlantCareBuddyContext _context;

        public CareEventService(PlantCareBuddyContext context)
        {
            _context = context;
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

        public async Task<CareEventDto> CreateCareEventAsync(CreateCareEventDto dto)
        {
            // Verify that the plant exists
            var plant = await _context.Plants.FindAsync(dto.PlantId);
            if (plant == null)
                throw new ArgumentException($"Plant with ID {dto.PlantId} not found");

            var careEvent = new CareEvent
            {
                PlantId = dto.PlantId,
                EventType = dto.EventType,
                EventDate = dto.EventDate,
                Notes = dto.Notes,
                ImagePath = dto.ImagePath
            };

            _context.CareEvents.Add(careEvent);
            await _context.SaveChangesAsync();

            // Reload the care event with plant data for the response
            await _context.Entry(careEvent).Reference(ce => ce.Plant).LoadAsync();

            return MapToDto(careEvent);
        }
        public async Task<CareEventDto?> UpdateCareEventAsync(int id, UpdateCareEventDto dto)
        {
            var careEvent = await _context.CareEvents
                .Include(ce => ce.Plant)
                .FirstOrDefaultAsync(ce => ce.Id == id);

            if (careEvent == null)
                return null;

            careEvent.EventType = dto.EventType;
            careEvent.EventDate = dto.EventDate;
            careEvent.Notes = dto.Notes;
            careEvent.ImagePath = dto.ImagePath;

            await _context.SaveChangesAsync();

            return MapToDto(careEvent);
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
                ImagePath = careEvent.ImagePath
            };
        }
    }
}