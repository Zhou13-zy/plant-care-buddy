namespace PlantCareBuddy.Domain.ValueObjects
{
    public class RecurrencePattern
    {
        public RecurrenceType Type { get; private set; }
        public int Interval { get; private set; }
        public DateTime? EndDate { get; private set; }
        public int? OccurrenceCount { get; private set; }
        public DayOfWeek[] DaysOfWeek { get; private set; }
        public int? DayOfMonth { get; private set; }

        private RecurrencePattern() { } // For EF

        public static RecurrencePattern Create(
            RecurrenceType type,
            int interval,
            DateTime? endDate = null,
            int? occurrenceCount = null,
            DayOfWeek[] daysOfWeek = null,
            int? dayOfMonth = null)
        {
            // General validation
            if (type == RecurrenceType.None)
                throw new ArgumentException("RecurrenceType cannot be None for a recurring reminder.");

            if (interval <= 0)
                throw new ArgumentException("Interval must be greater than zero.");

            if (endDate.HasValue && endDate.Value < DateTime.UtcNow.Date)
                throw new ArgumentException("EndDate cannot be in the past.");

            if (occurrenceCount.HasValue && occurrenceCount.Value <= 0)
                throw new ArgumentException("OccurrenceCount must be greater than zero.");

            // Type-specific validation
            if (type == RecurrenceType.Weekly)
            {
                if (daysOfWeek == null || daysOfWeek.Length == 0)
                    throw new ArgumentException("DaysOfWeek must be specified for weekly recurrence.");
            }

            if (type == RecurrenceType.Monthly)
            {
                if (!dayOfMonth.HasValue || dayOfMonth.Value < 1 || dayOfMonth.Value > 31)
                    throw new ArgumentException("DayOfMonth must be between 1 and 31 for monthly recurrence.");
            }

            if (type == RecurrenceType.Yearly)
            {
                if (!dayOfMonth.HasValue || dayOfMonth.Value < 1 || dayOfMonth.Value > 31)
                    throw new ArgumentException("DayOfMonth must be between 1 and 31 for yearly recurrence.");
            }

            return new RecurrencePattern
            {
                Type = type,
                Interval = interval,
                EndDate = endDate,
                OccurrenceCount = occurrenceCount,
                DaysOfWeek = daysOfWeek,
                DayOfMonth = dayOfMonth
            };
        }
    }
}