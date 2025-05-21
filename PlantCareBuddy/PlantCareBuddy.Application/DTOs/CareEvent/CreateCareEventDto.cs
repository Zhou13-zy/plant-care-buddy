using Microsoft.AspNetCore.Http;
using PlantCareBuddy.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PlantCareBuddy.Application.DTOs.CareEvent
{
    public class CreateCareEventDto
    {
        [Required]
        public Guid PlantId { get; set; }

        [Required]
        public CareEventType EventType { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        public string? Notes { get; set; }

        public IFormFile? BeforePhoto { get; set; }
        public IFormFile? AfterPhoto { get; set; }
    }
}