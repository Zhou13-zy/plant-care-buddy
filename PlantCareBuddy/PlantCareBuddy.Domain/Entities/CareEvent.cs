using PlantCareBuddy.Domain.Enums;
using System;

namespace PlantCareBuddy.Domain.Entities
{
    public class CareEvent
    {
        public Guid Id { get; set; }

        public Guid PlantId { get; set; }

        // Navigation property
        public virtual Plant Plant { get; set; }

        public CareEventType EventType { get; set; }
        public DateTime EventDate { get; set; }
        public string? Notes { get; set; }

        public string? BeforeImagePath { get; set; }
        public string? AfterImagePath { get; set; }
    }
}