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