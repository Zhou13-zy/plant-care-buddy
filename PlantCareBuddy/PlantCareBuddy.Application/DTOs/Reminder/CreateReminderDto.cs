using PlantCareBuddy.Domain.Enums;

namespace PlantCareBuddy.Application.DTOs.Reminder {
    public class CreateReminderDto
    {
        public Guid PlantId { get; set; }
        public ReminderType Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public RecurrencePatternDto Recurrence { get; set; }
        public CareIntensity Intensity { get; set; }
        public Guid? StrategyId { get; set; }
        public string StrategyParameters { get; set; }
        public bool IsStrategyOverride { get; set; }
    }
}