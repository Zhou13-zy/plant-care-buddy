namespace PlantCareBuddy.Application.DTOs.Dashboard
{
    public class UpcomingCareDto
    {
        public Guid PlantId { get; set; }
        public string PlantName { get; set; }
        public string CareType { get; set; }
        public DateTime DueDate { get; set; }
        public string StrategyName { get; set; }
        public bool IsOverdue { get; set; }
    }
}