using Microsoft.AspNetCore.Mvc;
using PlantCareBuddy.Application.Interfaces;
using PlantCareBuddy.Application.DTOs.Plant;
using PlantCareBuddy.Infrastructure.Interfaces.Storage;
using PlantCareBuddy.Domain.Enums;

namespace PlantCareBuddy.API.Controllers
{
    [ApiController]
    [Route("api/plants")]
    public class PlantController : ControllerBase
    {
        private readonly IPlantService _plantService;
        private readonly IPhotoStorageService _photoStorage;

        public PlantController(IPlantService plantService, IPhotoStorageService photoStorage)
        {
            _plantService = plantService;
            _photoStorage = photoStorage;
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
        public async Task<ActionResult<PlantDto>> CreatePlant([FromForm] CreatePlantDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdPlant = await _plantService.CreatePlantAsync(dto, _photoStorage);
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
        [HttpPut("{id}")]
        public async Task<ActionResult<PlantDto>> UpdatePlant(int id, [FromForm] UpdatePlantDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedPlant = await _plantService.UpdatePlantAsync(id, dto, _photoStorage);
            if (updatedPlant == null)
                return NotFound();

            return Ok(updatedPlant);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlant(int id)
        {
            var success = await _plantService.DeletePlantAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<PlantDto>>> SearchPlants(
            [FromQuery] string? name,
            [FromQuery] string? species,
            [FromQuery] PlantHealthStatus? healthStatus,
            [FromQuery] string? location)
        {
            var results = await _plantService.SearchPlantsAsync(name, species, healthStatus, location);
            return Ok(results);
        }
    }
}