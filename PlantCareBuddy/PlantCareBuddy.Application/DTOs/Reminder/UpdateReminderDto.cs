using PlantCareBuddy.Domain.Enums;

namespace PlantCareBuddy.Application.DTOs.Reminder
{
    public class UpdateReminderDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public RecurrencePatternDto Recurrence { get; set; }
        public CareIntensity Intensity { get; set; }
        public bool IsStrategyOverride { get; set; }
        public string StrategyParameters { get; set; }
    }
}