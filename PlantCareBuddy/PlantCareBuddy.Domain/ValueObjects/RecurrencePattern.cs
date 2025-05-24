namespace PlantCareBuddy.Domain.ValueObjects
{
    public enum RecurrenceType
    {
        None,
        Daily,
        Weekly,
        Monthly,
        Yearly,
        Custom
    }

    public class RecurrencePattern
    {
        public RecurrenceType Type { get; private set; }
        public int Interval { get; private set; } // e.g., every 3 days
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
            // Add validation as needed
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