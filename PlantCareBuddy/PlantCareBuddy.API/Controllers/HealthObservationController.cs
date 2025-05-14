using Microsoft.AspNetCore.Mvc;
using PlantCareBuddy.Application.DTOs.HealthObservation;
using PlantCareBuddy.Application.Interfaces;

namespace PlantCareBuddy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthObservationController : ControllerBase
    {
        private readonly IHealthObservationService _healthObservationService;

        public HealthObservationController(IHealthObservationService healthObservationService)
        {
            _healthObservationService = healthObservationService;
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
        public async Task<ActionResult<HealthObservationDto>> Create(CreateHealthObservationDto dto)
        {
            try
            {
                var observation = await _healthObservationService.CreateHealthObservationAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = observation.Id }, observation);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HealthObservationDto>> Update(int id, UpdateHealthObservationDto dto)
        {
            var observation = await _healthObservationService.UpdateHealthObservationAsync(id, dto);
            if (observation == null)
                return NotFound();

            return Ok(observation);
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