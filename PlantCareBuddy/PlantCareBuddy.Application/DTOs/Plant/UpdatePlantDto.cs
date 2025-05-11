using System;
using System.ComponentModel.DataAnnotations;
using PlantCareBuddy.Domain.Enums;

namespace PlantCareBuddy.Application.DTOs.Plant
{
    /// <summary>
    /// DTO for partially updating an existing plant.
    /// </summary>
    public class UpdatePlantDto
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(100)]
        public string? Species { get; set; }

        public DateTime? AcquisitionDate { get; set; }

        [MaxLength(200)]
        public string? Location { get; set; }

        public PlantHealthStatus? HealthStatus { get; set; }

        [MaxLength(1000)]
        public string? Notes { get; set; }

        [MaxLength(500)]
        public string? PrimaryImagePath { get; set; }
    }
}