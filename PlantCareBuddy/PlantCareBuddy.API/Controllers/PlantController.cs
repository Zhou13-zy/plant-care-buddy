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

        [HttpGet("{id}")]
        public async Task<ActionResult<PlantDto>> GetPlant(int id)
        {
            var plant = await _plantService.GetPlantByIdAsync(id);
            if (plant == null)
                return NotFound();
            return Ok(plant);
        }

        /// <summary>
        /// Adds a new plant to the collection.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<PlantDto>> CreatePlant([FromBody] CreatePlantDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdPlant = await _plantService.CreatePlantAsync(dto);
            return CreatedAtAction(nameof(GetPlants), new { id = createdPlant.Id }, createdPlant);
        }
        [HttpPost("batch")]
        public async Task<ActionResult<IEnumerable<PlantDto>>> CreatePlants([FromBody] List<CreatePlantDto> dtos)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdPlants = await _plantService.CreatePlantsAsync(dtos);
            return Ok(createdPlants);
        }
    }
}