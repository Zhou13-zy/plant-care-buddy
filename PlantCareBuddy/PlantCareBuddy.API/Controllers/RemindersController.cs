using Microsoft.AspNetCore.Mvc;
using PlantCareBuddy.Application.DTOs.Reminder;
using PlantCareBuddy.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class RemindersController : ControllerBase
{
    private readonly IReminderService _reminderService;

    public RemindersController(IReminderService reminderService)
    {
        _reminderService = reminderService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReminderDto dto)
    {
        var reminder = await _reminderService.CreateReminderAsync(dto);
        return Ok(reminder);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReminderDto>> Get(Guid id)
    {
        var reminder = await _reminderService.GetReminderByIdAsync(id);
        if (reminder == null) return NotFound();
        return Ok(reminder);
    }

    [HttpGet("plant/{plantId}")]
    public async Task<ActionResult<IEnumerable<ReminderDto>>> GetByPlant(Guid plantId)
    {
        var reminders = await _reminderService.GetRemindersByPlantIdAsync(plantId);
        return Ok(reminders);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReminderDto dto)
    {
        var updated = await _reminderService.UpdateReminderAsync(id, dto);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _reminderService.DeleteReminderAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }

    [HttpPost("{id}/complete")]
    public async Task<IActionResult> MarkAsComplete(Guid id)
    {
        var completed = await _reminderService.MarkAsCompletedAsync(id);
        if (completed == null) return NotFound();
        return Ok(completed);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReminderDto>>> GetAll()
    {
        var reminders = await _reminderService.GetAllRemindersAsync();
        return Ok(reminders);
    }

    [HttpPost("{plantId}/generate-strategy-reminders")]
    public async Task<ActionResult<IEnumerable<ReminderDto>>> GenerateStrategyReminders(Guid plantId)
    {
        var reminders = await _reminderService.GenerateStrategyRemindersAsync(plantId);
        return Ok(reminders);
    }
}