using PlantCareBuddy.Domain.Enums;
using System;

namespace PlantCareBuddy.Domain.Entities
{
    public class CareEvent
    {
        public int Id { get; set; }

        public int PlantId { get; set; }

        // Navigation property
        public virtual Plant Plant { get; set; }

        public CareEventType EventType { get; set; }
        public DateTime EventDate { get; set; }
        public string? Notes { get; set; }

        public string? ImagePath { get; set; }
    }
}