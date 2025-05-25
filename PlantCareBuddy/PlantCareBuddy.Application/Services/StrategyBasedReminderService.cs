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
                var nextWatering = DateTime.Today.AddDays(wateringInterval);
                var wateringRecurrence = RecurrencePattern.Create(
                    RecurrenceType.Daily, wateringInterval);

                reminders.Add(Reminder.Create(
                    plant.Id,
                    ReminderType.Watering,
                    "Watering",
                    $"Water your {plant.Name}",
                    nextWatering,
                    wateringRecurrence,
                    CareIntensity.Balanced,
                    strategyId: null,
                    strategyParameters: null,
                    isStrategyOverride: false
                ));
            }

            // Fertilizing Reminder
            int fertilizingInterval = strategy.GetFertilizingFrequencyDays(season);
            if (fertilizingInterval > 0)
            {
                var nextFertilizing = DateTime.Today.AddDays(fertilizingInterval);
                var fertilizingRecurrence = RecurrencePattern.Create(
                    RecurrenceType.Daily, fertilizingInterval);

                reminders.Add(Reminder.Create(
                    plant.Id,
                    ReminderType.Fertilizing,
                    "Fertilizing",
                    $"Fertilize your {plant.Name}",
                    nextFertilizing,
                    fertilizingRecurrence,
                    CareIntensity.Balanced,
                    strategyId: null,
                    strategyParameters: null,
                    isStrategyOverride: false
                ));
            }

            // Add more care types as needed (e.g., repotting, pruning, etc.)

            return reminders;
        }
    }
}