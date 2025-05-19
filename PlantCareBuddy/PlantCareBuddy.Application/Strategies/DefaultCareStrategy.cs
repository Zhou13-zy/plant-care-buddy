using PlantCareBuddy.Domain.Entities;
using PlantCareBuddy.Domain.Enums;
using PlantCareBuddy.Domain.Strategies;

namespace PlantCareBuddy.Application.Strategies
{
    public class DefaultCareStrategy : BaseCareStrategy
    {
        public override string Name => "General Plant Care";

        public override string Description => "Standard care for most common houseplants";

        protected override int GetBaseWateringFrequency() => 7; // Weekly watering

        protected override int GetBaseFertilizingFrequency() => 30; // Monthly fertilizing

        public override string GetLightRecommendation() =>
            "Medium indirect light - near a window but not in direct sunlight";

        public override string GetHumidityRecommendation() =>
            "Average home humidity (40-50%)";

        public override bool IsApplicableForPlant(Plant plant) =>
            plant.PlantType == PlantType.Default || plant.PlantType == PlantType.Indoor;
    }
}