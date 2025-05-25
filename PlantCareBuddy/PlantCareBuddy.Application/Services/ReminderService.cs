using Microsoft.EntityFrameworkCore;
using PlantCareBuddy.Application.DTOs.Plant;
using PlantCareBuddy.Application.DTOs.Reminder;
using PlantCareBuddy.Application.Interfaces;
using PlantCareBuddy.Application.Services;
using PlantCareBuddy.Domain.Entities;
using PlantCareBuddy.Domain.ValueObjects;
using PlantCareBuddy.Infrastructure.Persistence;

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
            dto.PlantId, dto.Type, dto.Title, dto.Description, dto.DueDate, recurrence,
            dto.Intensity, dto.StrategyId, dto.StrategyParameters, dto.IsStrategyOverride
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

        reminder.UpdateIntensity(dto.Intensity);
        reminder.UpdateStrategyParameters(dto.StrategyParameters);
        reminder.SetStrategyOverride(dto.IsStrategyOverride);

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
        reminder.MarkAsCompleted();
        await _reminderRepository.UpdateAsync(reminder);
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
                DayOfMonth = reminder.Recurrence.DayOfMonth
            },
            Intensity = reminder.Intensity,
            IsCompleted = reminder.IsCompleted,
            CompletedDate = reminder.CompletedDate,
            StrategyId = reminder.StrategyId,
            StrategyParameters = reminder.StrategyParameters,
            IsStrategyOverride = reminder.IsStrategyOverride,
            CreatedAt = reminder.CreatedAt,
            UpdatedAt = reminder.UpdatedAt
        };
    }
}