using PlantCareBuddy.Domain.Enums;
using PlantCareBuddy.Domain.ValueObjects;

namespace PlantCareBuddy.Application.DTOs.Reminder
{
    public class ReminderDto
    {
        public Guid Id { get; set; }
        public Guid PlantId { get; set; }
        public ReminderType Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public RecurrencePatternDto? Recurrence { get; set; }
        public CareIntensity Intensity { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedDate { get; set; }
        public Guid? StrategyId { get; set; }
        public string StrategyParameters { get; set; }
        public bool IsStrategyOverride { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}