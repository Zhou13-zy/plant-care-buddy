using PlantCareBuddy.Domain.Enums;

namespace PlantCareBuddy.Application.DTOs.CareEvent
{
    public class CareEventDto
    {
        public int Id { get; set; }
        public int PlantId { get; set; }
        public string PlantName { get; set; } // Include plant name for convenience
        public CareEventType EventType { get; set; }
        public string EventTypeName { get; set; } // Human-readable event type
        public DateTime EventDate { get; set; }
        public string? Notes { get; set; }
        public string? ImagePath { get; set; }
    }
}