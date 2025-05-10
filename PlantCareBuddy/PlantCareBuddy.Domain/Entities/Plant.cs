using System;
using System.ComponentModel.DataAnnotations;
using PlantCareBuddy.Domain.Enums;

namespace PlantCareBuddy.Domain.Entities
{
    /// <summary>
    /// Represents a plant in the user's collection.
    /// </summary>
    public class Plant
    {
        /// <summary>Primary key.</summary>
        public int Id { get; set; }

        /// <summary>Name of the plant.</summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>Species of the plant.</summary>
        [Required]
        [MaxLength(100)]
        public string Species { get; set; }

        /// <summary>Date the plant was acquired.</summary>
        [Required]
        public DateTime AcquisitionDate { get; set; }

        /// <summary>Location of the plant.</summary>
        [MaxLength(200)]
        public string Location { get; set; }

        /// <summary>Health status of the plant.</summary>
        [Required]
        public PlantHealthStatus HealthStatus { get; set; }

        /// <summary>Additional notes about the plant.</summary>
        [MaxLength(1000)]
        public string? Notes { get; set; }

        /// <summary>Path to the primary image of the plant.</summary>
        [MaxLength(500)]
        public string? PrimaryImagePath { get; set; }
    }
}