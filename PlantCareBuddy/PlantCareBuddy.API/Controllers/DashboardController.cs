using Microsoft.AspNetCore.Mvc;
using PlantCareBuddy.Application.DTOs.Dashboard;
using PlantCareBuddy.Application.Interfaces;

namespace PlantCareBuddy.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("overview")]
        public async Task<ActionResult<DashboardOverviewDto>> GetDashboardOverview()
        {
            var overview = await _dashboardService.GetDashboardOverviewAsync();
            return Ok(overview);
        }

        [HttpGet("plants-needing-attention")]
        public async Task<ActionResult<List<PlantNeedsAttentionDto>>> GetPlantsNeedingAttention()
        {
            var plants = await _dashboardService.GetPlantsNeedingAttentionAsync();
            return Ok(plants);
        }

        [HttpGet("upcoming-care")]
        public async Task<ActionResult<List<UpcomingCareDto>>> GetUpcomingCareTasks([FromQuery] int daysAhead = 7)
        {
            var tasks = await _dashboardService.GetUpcomingCareTasksAsync(daysAhead);
            return Ok(tasks);
        }

        [HttpGet("stats")]
        public async Task<ActionResult<DashboardStatsDto>> GetDashboardStats()
        {
            var stats = await _dashboardService.GetDashboardStatsAsync();
            return Ok(stats);
        }
    }
}