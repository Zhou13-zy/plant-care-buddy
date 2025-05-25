using PlantCareBuddy.Application.Interfaces;
using PlantCareBuddy.Domain.Entities;
using PlantCareBuddy.Domain.Enums;
using PlantCareBuddy.Domain.Interfaces;
using PlantCareBuddy.Domain.ValueObjects;

namespace PlantCareBuddy.Application.Services
{
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

            // Watering Reminder
            int wateringInterval = strategy.GetWateringFrequencyDays(season);
            if (wateringInterval > 0)
            {
                // Find the most recent watering event
                var lastWatering = plant.CareEvents?
                    .Where(e => e.EventType == CareEventType.Watering)
                    .OrderByDescending(e => e.EventDate)
                    .FirstOrDefault();

                DateTime nextWatering = lastWatering != null
                    ? lastWatering.EventDate.Date.AddDays(wateringInterval)
                    : DateTime.Today;
                var wateringRecurrence = RecurrencePattern.Create(
                    RecurrenceType.Custom, wateringInterval);

                reminders.Add(Reminder.Create(
                    plant.Id,
                    ReminderType.Watering,
                    "Watering",
                    $"Water your {plant.Name}",
                    nextWatering,
                    wateringRecurrence,
                    CareIntensity.Balanced,
                    strategyId: null,
                    strategyParameters: "",
                    isStrategyOverride: false
                ));
            }

            // Fertilizing Reminder
            int fertilizingInterval = strategy.GetFertilizingFrequencyDays(season);
            if (fertilizingInterval > 0)
            {
                // Find the most recent fertilizing event
                var lastFertilizing = plant.CareEvents?
                    .Where(e => e.EventType == CareEventType.Fertilizing)
                    .OrderByDescending(e => e.EventDate)
                    .FirstOrDefault();

                DateTime nextFertilizing = lastFertilizing != null
                    ? lastFertilizing.EventDate.Date.AddDays(fertilizingInterval)
                    : DateTime.Today;
                var fertilizingRecurrence = RecurrencePattern.Create(
                    RecurrenceType.Custom, fertilizingInterval);

                reminders.Add(Reminder.Create(
                    plant.Id,
                    ReminderType.Fertilizing,
                    "Fertilizing",
                    $"Fertilize your {plant.Name}",
                    nextFertilizing,
                    fertilizingRecurrence,
                    CareIntensity.Balanced,
                    strategyId: null,
                    strategyParameters: "",
                    isStrategyOverride: false
                ));
            }

            // Add more care types as needed (e.g., repotting, pruning, etc.)

            return reminders;
        }
    }
}