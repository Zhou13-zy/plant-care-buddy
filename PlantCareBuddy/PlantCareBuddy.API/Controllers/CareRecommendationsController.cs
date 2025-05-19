using Microsoft.AspNetCore.Mvc;
using PlantCareBuddy.Application.DTOs.Care;
using PlantCareBuddy.Application.Interfaces;
using PlantCareBuddy.Domain.Entities;

namespace PlantCareBuddy.API.Controllers
{
    [ApiController]
    [Route("api/plants/{plantId}/recommendations")]
    public class PlantRecommendationsController : ControllerBase
    {
        private readonly ICareStrategyService _careStrategyService;
        private readonly IPlantService _plantService;
        private readonly ICareEventService _careEventService; // Add this service

        public PlantRecommendationsController(
            ICareStrategyService careStrategyService,
            IPlantService plantService,
            ICareEventService careEventService) // Inject care event service
        {
            _careStrategyService = careStrategyService;
            _plantService = plantService;
            _careEventService = careEventService;
        }

        /// <summary>
        /// Get care recommendations for a specific plant
        /// </summary>
        /// <param name="plantId">ID of the plant</param>
        /// <returns>Care recommendations based on plant type and conditions</returns>
        [HttpGet]
        public async Task<ActionResult<CareRecommendationDto>> GetRecommendations(int plantId)
        {
            // Get the plant
            var plantDto = await _plantService.GetPlantByIdAsync(plantId);
            if (plantDto == null)
                return NotFound($"Plant with ID {plantId} not found");

            // Get care events for this plant separately
            var careEvents = await _careEventService.GetCareEventsByPlantIdAsync(plantId);

            // Map to domain entity
            var plantEntity = new Plant
            {
                Id = plantDto.Id,
                Name = plantDto.Name,
                PlantType = plantDto.PlantType,
                HealthStatus = plantDto.HealthStatus,
                // Add the care events we got separately
                CareEvents = careEvents.Select(ce => new CareEvent
                {
                    EventType = ce.EventType,
                    EventDate = ce.EventDate
                }).ToList()
            };

            // Generate recommendations
            var recommendations = _careStrategyService.GenerateRecommendation(plantEntity);
            return Ok(recommendations);
        }
    }
}