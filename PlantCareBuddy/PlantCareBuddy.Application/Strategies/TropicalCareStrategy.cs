using PlantCareBuddy.Domain.Entities;
using PlantCareBuddy.Domain.Enums;
using PlantCareBuddy.Domain.Strategies;

namespace PlantCareBuddy.Application.Strategies
{
    public class TropicalCareStrategy : BaseCareStrategy
    {
        public override string Name => "Tropical Plant Care";

        public override string Description => "Moisture-loving care for tropical plants";

        protected override int GetBaseWateringFrequency() => 5; // Every 5 days

        protected override int GetBaseFertilizingFrequency() => 14; // Every 2 weeks during growing season

        // Tropical plants are more sensitive to drying out in summer
        protected override int AdjustForSummer(int frequency) => (int)(frequency * 0.6); // Water much more often in summer

        public override string GetLightRecommendation() =>
            "Bright indirect light - shield from harsh direct sunlight";

        public override string GetHumidityRecommendation() =>
            "High humidity (60%+) - consider using a humidifier or pebble tray";

        public override bool IsApplicableForPlant(Plant plant) =>
            plant.PlantType == PlantType.Tropical;
    }
}