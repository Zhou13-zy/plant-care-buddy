using PlantCareBuddy.Application.Interfaces;
using PlantCareBuddy.Domain.Entities;
using PlantCareBuddy.Domain.Enums;
using PlantCareBuddy.Domain.Interfaces;

public class StrategyBasedReminderService : IStrategyBasedReminderService
{
    private readonly ICareStrategyService _careStrategyService;
    private readonly ISeasonService _seasonService;

    public StrategyBasedReminderService(
        ICareStrategyService careStrategyService,
        ISeasonService seasonService)
    {
        _careStrategyService = careStrategyService;
        _seasonService = seasonService;
    }

    public IEnumerable<Reminder> GenerateRemindersForPlant(Plant plant)
    {
        var reminders = new List<Reminder>();
        var strategy = _careStrategyService.GetStrategyForPlant(plant);
        var season = _seasonService.GetCurrentSeason();

        // Generate reminders for each reminder type
        foreach (ReminderType reminderType in Enum.GetValues(typeof(ReminderType)))
        {
            // Skip Custom and Note types as they're handled separately
            if (reminderType == ReminderType.Custom || reminderType == ReminderType.Note)
                continue;

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