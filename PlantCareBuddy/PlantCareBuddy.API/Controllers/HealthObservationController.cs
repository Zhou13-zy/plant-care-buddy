using Microsoft.AspNetCore.Mvc;
using PlantCareBuddy.Application.DTOs.HealthObservation;
using PlantCareBuddy.Application.Interfaces;
using PlantCareBuddy.Infrastructure.Interfaces.Storage;

namespace PlantCareBuddy.API.Controllers
{
    [ApiController]
    [Route("api/health-observations")]
    public class HealthObservationController : ControllerBase
    {
        private readonly IHealthObservationService _healthObservationService;
        private readonly IPhotoStorageService _photoStorage;

        public HealthObservationController(IHealthObservationService healthObservationService, IPhotoStorageService photoStorage)
        {
            _healthObservationService = healthObservationService;
            _photoStorage = photoStorage;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HealthObservationDto>>> GetAll()
        {
            var observations = await _healthObservationService.GetAllHealthObservationsAsync();
            return Ok(observations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HealthObservationDto>> GetById(int id)
        {
            var observation = await _healthObservationService.GetHealthObservationByIdAsync(id);
            if (observation == null)
                return NotFound();

            return Ok(observation);
        }

        [HttpGet("plant/{plantId}")]
        public async Task<ActionResult<IEnumerable<HealthObservationDto>>> GetByPlantId(int plantId)
        {
            var observations = await _healthObservationService.GetHealthObservationsByPlantIdAsync(plantId);
            return Ok(observations);
        }

        [HttpPost]
        public async Task<ActionResult<HealthObservationDto>> Create([FromForm] CreateHealthObservationDto createDto)
        {
            try
            {
                var observation = await _healthObservationService.CreateHealthObservationAsync(createDto, _photoStorage);
                return CreatedAtAction(nameof(GetById), new { id = observation.Id }, observation);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HealthObservationDto>> Update(int id, [FromForm] UpdateHealthObservationDto updateDto)
        {
            try
            {
                var observation = await _healthObservationService.UpdateHealthObservationAsync(id, updateDto, _photoStorage);
                if (observation == null)
                    return NotFound();

                return Ok(observation);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _healthObservationService.DeleteHealthObservationAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}