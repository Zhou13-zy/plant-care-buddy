using PlantCareBuddy.Domain.Entities;
using PlantCareBuddy.Domain.Enums;

namespace PlantCareBuddy.Domain.Strategies.Interfacces
{
    public interface ICareStrategy
    {
        /// <summary>
        /// The name of this care strategy
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Brief description of the care strategy
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Get recommended watering frequency in days
        /// </summary>
        int GetWateringFrequencyDays(Season season);

        /// <summary>
        /// Get recommended fertilizing frequency in days (0 means no fertilizing)
        /// </summary>
        int GetFertilizingFrequencyDays(Season season);

        /// <summary>
        /// Get light recommendation for the plant
        /// </summary>
        string GetLightRecommendation();

        /// <summary>
        /// Get humidity recommendation for the plant
        /// </summary>
        string GetHumidityRecommendation();

        /// <summary>
        /// Determine if this strategy applies to the given plant
        /// </summary>
        bool IsApplicableForPlant(Plant plant);
    }
}