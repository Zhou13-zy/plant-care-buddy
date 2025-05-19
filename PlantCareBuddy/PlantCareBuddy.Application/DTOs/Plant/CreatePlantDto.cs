using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using PlantCareBuddy.Domain.Enums;

namespace PlantCareBuddy.Application.DTOs.Plant
{
    /// <summary>
    /// DTO for creating a new plant.
    /// </summary>
    public class CreatePlantDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Species { get; set; }

        [Required]
        public PlantType PlantType { get; set; }

        [Required]
        public DateTime AcquisitionDate { get; set; }

        [MaxLength(200)]
        public string Location { get; set; }

        [Required]
        public PlantHealthStatus HealthStatus { get; set; }
        public DateTime? NextHealthCheckDate { get; set; }

        [MaxLength(1000)]
        public string? Notes { get; set; }

        [MaxLength(500)]
        public string? PrimaryImagePath { get; set; }
        public IFormFile? Photo { get; set; }
    }
}