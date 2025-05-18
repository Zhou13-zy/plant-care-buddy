using Microsoft.AspNetCore.Http;
using PlantCareBuddy.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PlantCareBuddy.Application.DTOs.HealthObservation
{
    public class UpdateHealthObservationDto
    {

        [Required]
        public DateTime ObservationDate { get; set; }

        [Required]
        public PlantHealthStatus HealthStatus { get; set; }

        [Required]
        public string Notes { get; set; }
        public IFormFile? Photo { get; set; }
    }
}