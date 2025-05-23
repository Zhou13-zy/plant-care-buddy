namespace PlantCareBuddy.Application.DTOs.Dashboard
{
    public class DashboardStatsDto
    {
        public int TotalPlants { get; set; }
        public int HealthyPlants { get; set; }
        public int PlantsNeedingAttention { get; set; }
        public int UpcomingCareTasks { get; set; }
    }
}