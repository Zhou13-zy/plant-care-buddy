using Microsoft.EntityFrameworkCore;
using PlantCareBuddy.Application.DTOs.Plant;
using PlantCareBuddy.Application.DTOs.Reminder;
using PlantCareBuddy.Application.Interfaces;
using PlantCareBuddy.Application.Services;
using PlantCareBuddy.Domain.Entities;
using PlantCareBuddy.Domain.Enums;
using PlantCareBuddy.Domain.ValueObjects;
using PlantCareBuddy.Infrastructure.Persistence;
using System.Diagnostics;

public class ReminderService : IReminderService
{
    private readonly IReminderRepository _reminderRepository;
    private readonly IStrategyBasedReminderService _strategyBasedReminderService;
    private readonly PlantCareBuddyContext _context;

    public ReminderService(
        IReminderRepository reminderRepository, 
        IStrategyBasedReminderService strategyBasedReminderService,
        PlantCareBuddyContext context)
    {
        _reminderRepository = reminderRepository;
        _strategyBasedReminderService = strategyBasedReminderService;
        _context = context;
    }

    public async Task<ReminderDto> CreateReminderAsync(CreateReminderDto dto)
    {
        RecurrencePattern? recurrence = null;
        if (dto.Recurrence != null)
        {
            recurrence = RecurrencePattern.Create(
                dto.Recurrence.Type, dto.Recurrence.Interval, dto.Recurrence.EndDate,
                dto.Recurrence.OccurrenceCount, dto.Recurrence.DaysOfWeek, dto.Recurrence.DayOfMonth
            );
        }
        var reminder = Reminder.Create(
            dto.PlantId, dto.Type, dto.Title, dto.Description, dto.DueDate, recurrence
        );
        await _reminderRepository.AddAsync(reminder);
        return MapToDto(reminder);
    }

    public async Task<ReminderDto?> GetReminderByIdAsync(Guid id)
    {
        var reminder = await _reminderRepository.GetByIdAsync(id);
        return reminder == null ? null : MapToDto(reminder);
    }

    public async Task<IEnumerable<ReminderDto>> GetRemindersByPlantIdAsync(Guid plantId)
    {
        var reminders = await _reminderRepository.GetByPlantIdAsync(plantId);
        return reminders.Select(MapToDto);
    }

    public async Task<ReminderDto?> UpdateReminderAsync(Guid id, UpdateReminderDto dto)
    {
        var reminder = await _reminderRepository.GetByIdAsync(id);
        if (reminder == null) return null;

        reminder.UpdateTitle(dto.Title);
        reminder.UpdateDescription(dto.Description);
        reminder.UpdateDueDate(dto.DueDate);

        RecurrencePattern? recurrence = null;
        if (dto.Recurrence != null)
        {
            recurrence = RecurrencePattern.Create(
                dto.Recurrence.Type, dto.Recurrence.Interval, dto.Recurrence.EndDate,
                dto.Recurrence.OccurrenceCount, dto.Recurrence.DaysOfWeek, dto.Recurrence.DayOfMonth
            );
        }
        reminder.UpdateRecurrence(recurrence);

        await _reminderRepository.UpdateAsync(reminder);
        return MapToDto(reminder);
    }

    public async Task<bool> DeleteReminderAsync(Guid id)
    {
        var reminder = await _reminderRepository.GetByIdAsync(id);
        if (reminder == null) return false;
        await _reminderRepository.DeleteAsync(id);
        return true;
    }

    public async Task<ReminderDto?> MarkAsCompletedAsync(Guid id)
    {
        var reminder = await _reminderRepository.GetByIdAsync(id);
        if (reminder == null) return null;

        // Create a care event for the completed reminder
        var careEvent = new CareEvent
        {
            PlantId = reminder.PlantId,
            EventType = (CareEventType)reminder.Type,
            EventDate = DateTime.UtcNow,
            Notes = $"Completed reminder: {reminder.Title}"
        };
        await _context.CareEvents.AddAsync(careEvent);

        if (reminder.Recurrence != null)
        {
            // For recurring reminders, calculate next occurrence from current date
            var nextDueDate = reminder.Recurrence.CalculateNextDueDate(DateTime.UtcNow);

            // Check if recurrence should end
            bool shouldEnd = false;
            
            // Check end date
            if (reminder.Recurrence.EndDate.HasValue && 
                reminder.Recurrence.EndDate.Value <= DateTime.UtcNow)
            {
                shouldEnd = true;
            }
            
            // Check occurrence count if not already ending and if it's set
            if (!shouldEnd && reminder.Recurrence.OccurrenceCount.HasValue)
            {
                // Get count of completed events for this reminder
                var completedCount = await _context.CareEvents
                    .CountAsync(e => e.PlantId == reminder.PlantId && 
                                   e.EventType == (CareEventType)reminder.Type);
                
                if (completedCount >= reminder.Recurrence.OccurrenceCount.Value)
                {
                    shouldEnd = true;
                }
            }

            if (shouldEnd)
            {
                // If recurrence should end, mark as completed
                reminder.MarkAsCompleted();
            }
            else
            {
                // For recurring reminders that should continue, reactivate with next due date
                reminder.ReactivateForNextOccurrence(nextDueDate);
            }
        }
        else
        {
            // For non-recurring reminders, just mark as completed
            reminder.MarkAsCompleted();
        }

        // Save all changes (both the care event and the reminder update)
        await _context.SaveChangesAsync();
        return MapToDto(reminder);
    }

    public async Task<IEnumerable<ReminderDto>> GetAllRemindersAsync()
    {
        var reminders = await _reminderRepository.GetAllAsync();
        return reminders.Select(MapToDto);
    }

    public async Task<IEnumerable<ReminderDto>> GenerateStrategyRemindersAsync(Guid plantId)
    {
        var plant = await _context.Plants
            .Include(p => p.CareEvents)
            .FirstOrDefaultAsync(p => p.Id == plantId);

        if (plant == null)
            throw new Exception("Plant not found.");

        var reminders = _strategyBasedReminderService.GenerateRemindersForPlant(plant);

        foreach (var reminder in reminders)
        {
            await _reminderRepository.AddAsync(reminder);
        }
        await _context.SaveChangesAsync();

        return reminders.Select(MapToDto);
    }

    private static ReminderDto MapToDto(Reminder reminder)
    {
        return new ReminderDto
        {
            Id = reminder.Id,
            PlantId = reminder.PlantId,
            PlantName = reminder.Plant.Name,
            Type = reminder.Type,
            Title = reminder.Title,
            Description = reminder.Description,
            DueDate = reminder.DueDate,
            // Recurrence can be null if this reminder has no recurrence pattern
            Recurrence = reminder.Recurrence == null ? null : new RecurrencePatternDto
            {
                Type = reminder.Recurrence.Type,
                Interval = reminder.Recurrence.Interval,
                EndDate = reminder.Recurrence.EndDate,
                OccurrenceCount = reminder.Recurrence.OccurrenceCount,
                DaysOfWeek = reminder.Recurrence.DaysOfWeek,
                DayOfMonth = reminder.Recurrence.DayOfMonth,
                MonthOfYear = reminder.Recurrence.MonthOfYear
            },
            IsCompleted = reminder.IsCompleted,
            CompletedDate = reminder.CompletedDate,
            CreatedAt = reminder.CreatedAt,
            UpdatedAt = reminder.UpdatedAt
        };
    }
}