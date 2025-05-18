using Microsoft.AspNetCore.Mvc;
using PlantCareBuddy.Application.DTOs.CareEvent;
using PlantCareBuddy.Application.Interfaces;
using PlantCareBuddy.Infrastructure.Interfaces.Storage;

namespace PlantCareBuddy.API.Controllers
{
    [ApiController]
    [Route("api/care-events")]
    public class CareEventController : ControllerBase
    {
        private readonly ICareEventService _careEventService;
        private readonly IPhotoStorageService _photoStorage;

        public CareEventController(ICareEventService careEventService, IPhotoStorageService photoStorage)
        {
            _careEventService = careEventService;
            _photoStorage = photoStorage;
        }

        /// <summary>
        /// Gets all care events.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CareEventDto>>> GetCareEvents()
        {
            var careEvents = await _careEventService.GetAllCareEventsAsync();
            return Ok(careEvents);
        }

        /// <summary>
        /// Gets care events for a specific plant.
        /// </summary>
        [HttpGet("plant/{plantId}")]
        public async Task<ActionResult<IEnumerable<CareEventDto>>> GetCareEventsByPlant(int plantId)
        {
            var careEvents = await _careEventService.GetCareEventsByPlantIdAsync(plantId);
            return Ok(careEvents);
        }

        /// <summary>
        /// Gets a specific care event by ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<CareEventDto>> GetCareEvent(int id)
        {
            var careEvent = await _careEventService.GetCareEventByIdAsync(id);
            if (careEvent == null)
                return NotFound();

            return Ok(careEvent);
        }
        /// <summary>
        /// Creates a new care event.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CareEventDto>> CreateCareEvent([FromForm] CreateCareEventDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdCareEvent = await _careEventService.CreateCareEventAsync(dto, _photoStorage);
                return CreatedAtAction(nameof(GetCareEvent), new { id = createdCareEvent.Id }, createdCareEvent);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Updates an existing care event.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<CareEventDto>> UpdateCareEvent(int id, [FromForm] UpdateCareEventDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedCareEvent = await _careEventService.UpdateCareEventAsync(id, dto, _photoStorage);
            if (updatedCareEvent == null)
                return NotFound();

            return Ok(updatedCareEvent);
        }
        /// <summary>
        /// Deletes a care event.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCareEvent(int id)
        {
            var success = await _careEventService.DeleteCareEventAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}