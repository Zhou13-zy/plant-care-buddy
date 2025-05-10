using Microsoft.AspNetCore.Mvc;
using PlantCareBuddy.Application.Interfaces;
using PlantCareBuddy.Application.DTOs.Plant;

namespace PlantCareBuddy.API.Controllers
{
    [ApiController]
    [Route("api/plants")]
    public class PlantController : ControllerBase
    {
        private readonly IPlantService _plantService;

        public PlantController(IPlantService plantService)
        {
            _plantService = plantService;
        }

        /// <summary>
        /// Gets all plants in the collection.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlantDto>>> GetPlants()
        {
            var plants = await _plantService.GetAllPlantsAsync();
            return Ok(plants);
        }
    }
}