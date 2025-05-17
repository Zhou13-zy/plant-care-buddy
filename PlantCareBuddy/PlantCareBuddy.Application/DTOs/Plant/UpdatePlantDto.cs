using System.ComponentModel.DataAnnotations;

namespace PlantCareBuddy.Application.DTOs.Plant
{
    /// <summary>
    /// DTO for partially updating an existing plant.
    /// Health status is not directly updatable and is derived from health observations.
    /// </summary>
    public class UpdatePlantDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Species { get; set; }

        [Required]
        public DateTime AcquisitionDate { get; set; }

        [Required]
        [MaxLength(200)]
        public string Location { get; set; }

        public DateTime? NextHealthCheckDate { get; set; }

        [MaxLength(1000)]
        public string? Notes { get; set; }

        [MaxLength(500)]
        public string? PrimaryImagePath { get; set; }
    }
}