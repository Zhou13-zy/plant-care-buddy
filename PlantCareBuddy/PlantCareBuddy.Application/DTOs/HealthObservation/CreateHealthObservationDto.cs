using System.ComponentModel.DataAnnotations;
using PlantCareBuddy.Domain.Enums;

namespace PlantCareBuddy.Application.DTOs.HealthObservation
{
    public class CreateHealthObservationDto
    {
        [Required]
        public int PlantId { get; set; }
        [Required]
        public DateTime ObservationDate { get; set; }
        [Required]
        public PlantHealthStatus HealthStatus { get; set; }
        [Required]
        public string Notes { get; set; }
        public string? ImagePath { get; set; }
    }
}