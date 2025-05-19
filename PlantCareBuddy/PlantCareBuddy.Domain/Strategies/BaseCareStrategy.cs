using PlantCareBuddy.Domain.Entities;
using PlantCareBuddy.Domain.Enums;
using PlantCareBuddy.Domain.Strategies.Interfacces;

namespace PlantCareBuddy.Domain.Strategies
{
    public abstract class BaseCareStrategy : ICareStrategy
    {
        /// <summary>
        /// The name of this care strategy
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Brief description of the care strategy
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Get recommended watering frequency in days
        /// </summary>
        public virtual int GetWateringFrequencyDays(Season season)
        {
            // Get the base frequency for this plant type
            int baseFrequency = GetBaseWateringFrequency();

            // Apply seasonal adjustments
            return season switch
            {
                Season.Summer => AdjustForSummer(baseFrequency),
                Season.Winter => AdjustForWinter(baseFrequency),
                _ => baseFrequency // Spring/Autumn use base frequency
            };
        }

        /// <summary>
        /// Get recommended fertilizing frequency in days
        /// </summary>
        public virtual int GetFertilizingFrequencyDays(Season season)
        {
            // Most plants don't need fertilizer in winter
            if (season == Season.Winter && ShouldSkipWinterFeeding())
                return 0;

            return GetBaseFertilizingFrequency();
        }

        /// <summary>
        /// Get the base watering frequency in days for this plant type
        /// </summary>
        protected abstract int GetBaseWateringFrequency();

        /// <summary>
        /// Get the base fertilizing frequency in days for this plant type
        /// </summary>
        protected abstract int GetBaseFertilizingFrequency();

        /// <summary>
        /// Determine whether fertilizing should be skipped in winter
        /// </summary>
        protected virtual bool ShouldSkipWinterFeeding() => true;

        /// <summary>
        /// Adjust watering frequency for summer (typically increase frequency)
        /// </summary>
        protected virtual int AdjustForSummer(int frequency) => (int)(frequency * 0.7); // Water ~30% more often

        /// <summary>
        /// Adjust watering frequency for winter (typically decrease frequency)
        /// </summary>
        protected virtual int AdjustForWinter(int frequency) => (int)(frequency * 1.5); // Water ~50% less often

        /// <summary>
        /// Get light recommendation for the plant
        /// </summary>
        public abstract string GetLightRecommendation();

        /// <summary>
        /// Get humidity recommendation for the plant
        /// </summary>
        public abstract string GetHumidityRecommendation();

        /// <summary>
        /// Determine if this strategy applies to the given plant
        /// </summary>
        public abstract bool IsApplicableForPlant(Plant plant);
    }
}