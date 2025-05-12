using Microsoft.AspNetCore.Mvc;
using PlantCareBuddy.Application.DTOs.CareEvent;
using PlantCareBuddy.Application.Interfaces;

namespace PlantCareBuddy.API.Controllers
{
    [ApiController]
    [Route("api/care-events")]
    public class CareEventController : ControllerBase
    {
        private readonly ICareEventService _careEventService;

        public CareEventController(ICareEventService careEventService)
        {
            _careEventService = careEventService;
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
        public async Task<ActionResult<CareEventDto>> CreateCareEvent([FromBody] CreateCareEventDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdCareEvent = await _careEventService.CreateCareEventAsync(dto);
                return CreatedAtAction(nameof(GetCareEvent), new { id = createdCareEvent.Id }, createdCareEvent);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}