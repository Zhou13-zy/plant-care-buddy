using PlantCareBuddy.Application.Interfaces;
using PlantCareBuddy.Domain.Entities;
using PlantCareBuddy.Domain.Enums;
using PlantCareBuddy.Domain.Interfaces;

public class StrategyBasedReminderService : IStrategyBasedReminderService
{
    private readonly ICareStrategyService _careStrategyService;
    private readonly ISeasonService _seasonService;
    private readonly IReminderRepository _reminderRepository;

    public StrategyBasedReminderService(
        ICareStrategyService careStrategyService,
        ISeasonService seasonService,
        IReminderRepository reminderRepository)
    {
        _careStrategyService = careStrategyService;
        _seasonService = seasonService;
        _reminderRepository = reminderRepository;
    }

    public async Task<IEnumerable<Reminder>> GenerateRemindersForPlantAsync(Plant plant)
    {
        var reminders = new List<Reminder>();
        // Get all active (not completed) reminders for this plant
        var existingReminders = plant.Reminders.Where(r => !r.IsCompleted).ToList();
        var season = _seasonService.GetCurrentSeason();
        var strategy = _careStrategyService.GetStrategyForPlant(plant);

        foreach (ReminderType reminderType in Enum.GetValues(typeof(ReminderType)))
        {
            // Skip Custom and Note types as they're handled separately
            if (reminderType == ReminderType.Custom || reminderType == ReminderType.Note)
                continue;

            // Check for duplicate
            if (await _reminderRepository.ActiveReminderExistsAsync(plant.Id, reminderType))
                continue; // Skip creating this reminder

            var recurrence = strategy.GetCareRecurrence(reminderType, season);
            if (recurrence == null)
                continue;

            // Find the most recent care event of this type
            CareEvent? lastEvent = null;
            if (IsCareEventType(reminderType))
            {
                lastEvent = plant.CareEvents?
                    .Where(e => (CareEventType)reminderType == e.EventType)
                    .OrderByDescending(e => e.EventDate)
                    .FirstOrDefault();
            }

            // Calculate next due date
            DateTime nextDueDate = lastEvent != null
                ? recurrence.CalculateNextDueDate(lastEvent.EventDate)
                : DateTime.Today;

            // Fix: Prevent due date in the past
            if (nextDueDate < DateTime.Today)
                nextDueDate = DateTime.Today;

            // Create the reminder
            reminders.Add(Reminder.Create(
                plant.Id,
                reminderType,
                GetReminderTitle(reminderType, plant.Name),
                GetReminderDescription(reminderType, plant.Name),
                nextDueDate,
                recurrence
            ));
        }

        return reminders;
    }

    private string GetReminderTitle(ReminderType type, string plantName)
    {
        return type switch
        {
            ReminderType.Watering => "Watering",
            ReminderType.Fertilizing => "Fertilizing",
            ReminderType.Repotting => "Repotting",
            ReminderType.Pruning => "Pruning",
            ReminderType.PestTreatment => "Pest Treatment",
            ReminderType.Cleaning => "Cleaning",
            ReminderType.Misting => "Misting",
            ReminderType.Rotating => "Rotating",
            ReminderType.Inspection => "Plant Inspection",
            ReminderType.Note => "Note",
            ReminderType.Custom => "Custom Reminder",
            _ => type.ToString()
        };
    }

    private string GetReminderDescription(ReminderType type, string plantName)
    {
        return type switch
        {
            ReminderType.Watering => $"Time to water your {plantName}",
            ReminderType.Fertilizing => $"Time to fertilize your {plantName}",
            ReminderType.Repotting => $"Your {plantName} might need repotting",
            ReminderType.Pruning => $"Time to prune your {plantName}",
            ReminderType.PestTreatment => $"Check and treat {plantName} for pests",
            ReminderType.Cleaning => $"Clean the leaves of {plantName}",
            ReminderType.Misting => $"Mist your {plantName} for humidity",
            ReminderType.Rotating => $"Rotate {plantName} for even growth",
            ReminderType.Inspection => $"Inspect {plantName} for overall health",
            ReminderType.Note => $"Note for {plantName}",
            ReminderType.Custom => $"Custom reminder for {plantName}",
            _ => $"Care reminder for your {plantName}"
        };
    }

    private bool IsCareEventType(ReminderType type)
    {
        return Enum.TryParse<CareEventType>(type.ToString(), out _);
    }
}