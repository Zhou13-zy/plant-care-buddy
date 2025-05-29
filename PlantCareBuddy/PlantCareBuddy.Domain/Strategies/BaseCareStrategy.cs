using PlantCareBuddy.Domain.Entities;
using PlantCareBuddy.Domain.Enums;
using PlantCareBuddy.Domain.Strategies.Interfacces;
using PlantCareBuddy.Domain.ValueObjects;

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

        public virtual RecurrencePattern GetCareRecurrence(ReminderType reminderType, Season season)
        {
            return reminderType switch
            {
                ReminderType.Watering => GetWateringRecurrence(season),
                ReminderType.Fertilizing => GetFertilizingRecurrence(season),
                ReminderType.Repotting => GetRepottingRecurrence(season),
                ReminderType.Pruning => GetPruningRecurrence(season),
                ReminderType.PestTreatment => GetPestTreatmentRecurrence(season),
                ReminderType.Cleaning => GetCleaningRecurrence(season),
                ReminderType.Misting => GetMistingRecurrence(season),
                ReminderType.Rotating => GetRotatingRecurrence(season),
                ReminderType.Inspection => GetInspectionRecurrence(season),
                ReminderType.Custom => null, // Custom reminders are handled separately
                ReminderType.Note => null,   // Notes don't have recurrence
                _ => throw new ArgumentException($"Unsupported reminder type: {reminderType}")
            } ?? throw new InvalidOperationException("RecurrencePattern cannot be null.");
        }

        protected virtual RecurrencePattern GetWateringRecurrence(Season season)
        {
            return season switch
            {
                Season.Summer => RecurrencePattern.Create(
                    RecurrenceType.Weekly,
                    interval: 1,
                    daysOfWeek: new[] { DayOfWeek.Monday, DayOfWeek.Thursday }
                ),
                Season.Winter => RecurrencePattern.Create(
                    RecurrenceType.Weekly,
                    interval: 1,
                    daysOfWeek: new[] { DayOfWeek.Wednesday }
                ),
                _ => RecurrencePattern.Create(
                    RecurrenceType.Weekly,
                    interval: 1,
                    daysOfWeek: new[] { DayOfWeek.Monday, DayOfWeek.Friday }
                )
            };
        }

        protected virtual RecurrencePattern GetFertilizingRecurrence(Season season)
        {
            if (season == Season.Winter && ShouldSkipWinterFeeding())
                return null;

            return RecurrencePattern.Create(
                RecurrenceType.Monthly,
                interval: 1,
                dayOfMonth: 1
            );
        }

        protected virtual RecurrencePattern GetRepottingRecurrence(Season season)
        {
            return RecurrencePattern.Create(
                RecurrenceType.Yearly,
                interval: 1,
                dayOfMonth: 1,
                monthOfYear: 5 // Example: May 1st every year
            );
        }

        protected virtual RecurrencePattern GetPruningRecurrence(Season season)
        {
            return RecurrencePattern.Create(
                RecurrenceType.Monthly,
                interval: 3,
                dayOfMonth: 1
            );
        }

        protected virtual RecurrencePattern GetPestTreatmentRecurrence(Season season)
        {
            // Example: Pest treatment every 2 weeks
            return RecurrencePattern.Create(
                RecurrenceType.Weekly,
                interval: 2,
                daysOfWeek: new[] { DayOfWeek.Saturday }
            );
        }

        protected virtual RecurrencePattern GetCleaningRecurrence(Season season)
        {
            // Example: Cleaning every month on the 1st
            return RecurrencePattern.Create(
                RecurrenceType.Monthly,
                interval: 1,
                dayOfMonth: 1
            );
        }

        protected virtual RecurrencePattern GetMistingRecurrence(Season season)
        {
            // Example: Misting every 3 days
            return RecurrencePattern.Create(
                RecurrenceType.Daily,
                interval: 3
            );
        }

        protected virtual RecurrencePattern GetRotatingRecurrence(Season season)
        {
            // Example: Rotating every week on Sunday
            return RecurrencePattern.Create(
                RecurrenceType.Weekly,
                interval: 1,
                daysOfWeek: new[] { DayOfWeek.Sunday }
            );
        }

        protected virtual RecurrencePattern GetInspectionRecurrence(Season season)
        {
            return RecurrencePattern.Create(
                RecurrenceType.Weekly,
                interval: 1,
                daysOfWeek: new[] { DayOfWeek.Sunday }
            );
        }
    }
}