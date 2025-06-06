﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using PlantCareBuddy.Domain.Enums;

namespace PlantCareBuddy.Application.DTOs.HealthObservation
{
    public class CreateHealthObservationDto
    {
        [Required]
        public Guid PlantId { get; set; }
        [Required]
        public DateTime ObservationDate { get; set; }
        [Required]
        public PlantHealthStatus HealthStatus { get; set; }
        [Required]
        public string Notes { get; set; }
        public IFormFile? Photo { get; set; }
    }
}