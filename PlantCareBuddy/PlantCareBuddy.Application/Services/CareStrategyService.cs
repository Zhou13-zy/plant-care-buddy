using PlantCareBuddy.Application.DTOs.Care;
using PlantCareBuddy.Application.Interfaces;
using PlantCareBuddy.Application.Strategies;
using PlantCareBuddy.Domain.Entities;
using PlantCareBuddy.Domain.Enums;
using PlantCareBuddy.Domain.Interfaces;
using PlantCareBuddy.Domain.Strategies.Interfacces;

namespace PlantCareBuddy.Application.Services
{
    public class CareStrategyService : ICareStrategyService
    {
        private readonly IEnumerable<ICareStrategy> _strategies;
        private readonly ISeasonService _seasonService;

        public CareStrategyService(
            IEnumerable<ICareStrategy> strategies,
            ISeasonService _seasonService)
        {
            _strategies = strategies ?? throw new ArgumentNullException(nameof(strategies));
            this._seasonService = _seasonService ?? throw new ArgumentNullException(nameof(_seasonService));
        }

        /// <summary>
        /// Get the appropriate care strategy for a plant
        /// </summary>
        public ICareStrategy GetStrategyForPlant(Plant plant)
        {
            // Try to find a specific strategy for this plant type
            var strategy = _strategies.FirstOrDefault(s => s.IsApplicableForPlant(plant));

            // If no specific strategy found, use the default strategy
            if (strategy == null)
            {
                strategy = _strategies.FirstOrDefault(s => s is DefaultCareStrategy)
                    ?? throw new InvalidOperationException("Default care strategy not found. Ensure DefaultCareStrategy is registered.");
            }

            return strategy;
        }

        /// <summary>
        /// Generate care recommendations for a specific plant
        /// </summary>
        public CareRecommendationDto GenerateRecommendation(Plant plant)
        {
            var strategy = GetStrategyForPlant(plant);
            var currentSeason = _seasonService.GetCurrentSeason();

            // Calculate intervals
            int wateringInterval = strategy.GetWateringFrequencyDays(currentSeason);
            int fertilizingInterval = strategy.GetFertilizingFrequencyDays(currentSeason);

            // Calculate next dates based on most recent care events
            DateTime nextWateringDate = CalculateNextWateringDate(plant, wateringInterval);
            DateTime? nextFertilizingDate = fertilizingInterval > 0
                ? CalculateNextFertilizingDate(plant, fertilizingInterval)
                : null;

            return new CareRecommendationDto
            {
                PlantId = plant.Id,
                PlantName = plant.Name,
                StrategyName = strategy.Name,
                StrategyDescription = strategy.Description,
                WateringIntervalDays = wateringInterval,
                FertilizingIntervalDays = fertilizingInterval,
                NextWateringDate = nextWateringDate,
                NextFertilizingDate = nextFertilizingDate,
                LightRecommendation = strategy.GetLightRecommendation(),
                HumidityRecommendation = strategy.GetHumidityRecommendation(),
                CurrentSeason = currentSeason.ToString()
            };
        }

        /// <summary>
        /// Calculate the next watering date based on the interval and last watering
        /// </summary>
        private DateTime CalculateNextWateringDate(Plant plant, int wateringInterval)
        {
            // Find the most recent watering event
            var lastWatering = plant.CareEvents
                .Where(ce => ce.EventType == CareEventType.Watering)
                .OrderByDescending(ce => ce.EventDate)
                .FirstOrDefault();

            // If plant has been watered before, calculate next date from last watering
            if (lastWatering != null)
            {
                return lastWatering.EventDate.AddDays(wateringInterval);
            }

            // If no watering history, suggest watering today
            return DateTime.Today;
        }

        /// <summary>
        /// Calculate the next fertilizing date based on the interval and last fertilizing
        /// </summary>
        private DateTime CalculateNextFertilizingDate(Plant plant, int fertilizingInterval)
        {
            // Find the most recent fertilizing event
            var lastFertilizing = plant.CareEvents
                .Where(ce => ce.EventType == CareEventType.Fertilizing)
                .OrderByDescending(ce => ce.EventDate)
                .FirstOrDefault();

            // If plant has been fertilized before, calculate next date from last fertilizing
            if (lastFertilizing != null)
            {
                return lastFertilizing.EventDate.AddDays(fertilizingInterval);
            }

            // If no fertilizing history, suggest fertilizing today
            return DateTime.Today;
        }
    }
}