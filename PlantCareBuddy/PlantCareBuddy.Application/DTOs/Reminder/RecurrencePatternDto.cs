using PlantCareBuddy.Domain.ValueObjects;

public class RecurrencePatternDto
{
    public RecurrenceType Type { get; set; }
    public int Interval { get; set; }
    public DateTime? EndDate { get; set; }
    public int? OccurrenceCount { get; set; }
    public DayOfWeek[] DaysOfWeek { get; set; }
    public int? DayOfMonth { get; set; }
}