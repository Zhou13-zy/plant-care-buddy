using PlantCareBuddy.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace PlantCareBuddy.Application.DTOs.CareEvent
{
    public class UpdateCareEventDto
    {
        [Required]
        public CareEventType EventType { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        public string? Notes { get; set; }

        public string? ImagePath { get; set; }
    }
}