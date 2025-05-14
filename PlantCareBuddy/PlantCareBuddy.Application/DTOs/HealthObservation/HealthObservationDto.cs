using PlantCareBuddy.Domain.Enums;

namespace PlantCareBuddy.Application.DTOs.HealthObservation
{
    public class HealthObservationDto
    {
        public int Id { get; set; }
        public int PlantId { get; set; }
        public string PlantName { get; set; }
        public DateTime ObservationDate { get; set; }
        public PlantHealthStatus HealthStatus { get; set; }
        public string HealthStatusName { get; set; }
        public string Notes { get; set; }
        public string? ImagePath { get; set; }
    }
}