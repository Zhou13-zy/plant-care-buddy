using PlantCareBuddy.Application.DTOs.Care;
using PlantCareBuddy.Domain.Entities;
using PlantCareBuddy.Domain.Strategies.Interfacces;

namespace PlantCareBuddy.Application.Interfaces
{
    public interface ICareStrategyService
    {
        /// <summary>
        /// Get the appropriate care strategy for a plant
        /// </summary>
        /// <param name="plant">The plant to get a strategy for</param>
        /// <returns>The selected care strategy</returns>
        ICareStrategy GetStrategyForPlant(Plant plant);

        /// <summary>
        /// Generate care recommendations for a specific plant
        /// </summary>
        /// <param name="plant">The plant to generate recommendations for</param>
        /// <returns>Care recommendations based on plant type and conditions</returns>
        CareRecommendationDto GenerateRecommendation(Plant plant);
    }
}