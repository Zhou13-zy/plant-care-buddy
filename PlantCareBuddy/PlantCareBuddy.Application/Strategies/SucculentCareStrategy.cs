using PlantCareBuddy.Domain.Entities;
using PlantCareBuddy.Domain.Enums;
using PlantCareBuddy.Domain.Strategies;
using PlantCareBuddy.Domain.ValueObjects;

namespace PlantCareBuddy.Application.Strategies
{
    public class SucculentCareStrategy : BaseCareStrategy
    {
        public override string Name => "Succulent & Cacti Care";

        public override string Description => "Water-conserving care for drought-tolerant plants";

        protected override int GetBaseWateringFrequency() => 14; // Every 2 weeks

        protected override int GetBaseFertilizingFrequency() => 60; // Every 2 months during growing season

        protected override int AdjustForSummer(int frequency) => (int)(frequency * 0.8); // Less extreme than other plants

        protected override int AdjustForWinter(int frequency) => frequency * 2; // Half as often in winter

        public override string GetLightRecommendation() =>
            "Bright direct or indirect light - at least 6 hours of sunlight daily";

        public override string GetHumidityRecommendation() =>
            "Low humidity (30-40%) - avoid humid environments";

        public override bool IsApplicableForPlant(Plant plant) =>
            plant.PlantType == PlantType.Succulent;

        protected override RecurrencePattern GetWateringRecurrence(Season season)
        {
            return season switch
            {
                Season.Summer => RecurrencePattern.Create(
                    RecurrenceType.Weekly,
                    interval: 2,
                    daysOfWeek: new[] { DayOfWeek.Monday }
                ),
                Season.Winter => RecurrencePattern.Create(
                    RecurrenceType.Weekly,
                    interval: 4,
                    daysOfWeek: new[] { DayOfWeek.Monday }
                ),
                _ => RecurrencePattern.Create(
                    RecurrenceType.Weekly,
                    interval: 2,
                    daysOfWeek: new[] { DayOfWeek.Monday }
                )
            };
        }

        protected override RecurrencePattern GetFertilizingRecurrence(Season season)
        {
            if (season == Season.Winter)
                return null;

            return RecurrencePattern.Create(
                RecurrenceType.Monthly,
                interval: 2,
                dayOfMonth: 1
            );
        }
    }
}