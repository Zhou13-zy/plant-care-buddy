using PlantCareBuddy.Application.DTOs.Dashboard;

namespace PlantCareBuddy.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardOverviewDto> GetDashboardOverviewAsync();
        Task<List<PlantNeedsAttentionDto>> GetPlantsNeedingAttentionAsync();
        Task<List<UpcomingCareDto>> GetUpcomingCareTasksAsync(int daysAhead = 7);
        Task<DashboardStatsDto> GetDashboardStatsAsync();
    }
}