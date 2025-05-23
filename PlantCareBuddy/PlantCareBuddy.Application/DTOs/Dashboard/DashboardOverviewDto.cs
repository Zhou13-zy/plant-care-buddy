namespace PlantCareBuddy.Application.DTOs.Dashboard
{
    public class DashboardOverviewDto
    {
        public List<PlantNeedsAttentionDto> PlantsNeedingAttention { get; set; }
        public List<UpcomingCareDto> UpcomingCareTasks { get; set; }
        public DashboardStatsDto Stats { get; set; }
    }
}