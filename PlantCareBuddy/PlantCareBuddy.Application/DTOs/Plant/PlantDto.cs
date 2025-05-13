using PlantCareBuddy.Domain.Enums;

namespace PlantCareBuddy.Application.DTOs.Plant
{
    /// <summary>
    /// Data Transfer Object for Plant entity.
    /// </summary>
    public class PlantDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public string Location { get; set; }
        public DateTime AcquisitionDate { get; set; }
        public PlantHealthStatus HealthStatus { get; set; }
        public string? Notes { get; set; }
        public string? PrimaryImagePath { get; set; }
    }
}