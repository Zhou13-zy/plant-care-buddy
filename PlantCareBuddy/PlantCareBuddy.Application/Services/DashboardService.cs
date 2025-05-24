// PlantCareBuddy.Application/Services/DashboardService.cs
using Microsoft.EntityFrameworkCore;
using PlantCareBuddy.Application.DTOs.Care;
using PlantCareBuddy.Application.DTOs.Dashboard;
using PlantCareBuddy.Application.Interfaces;
using PlantCareBuddy.Domain.Entities;
using PlantCareBuddy.Domain.Enums;
using PlantCareBuddy.Infrastructure.Persistence;

namespace PlantCareBuddy.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly PlantCareBuddyContext _context;
        private readonly ICareStrategyService _strategyService;

        public DashboardService(
            PlantCareBuddyContext context,
            ICareStrategyService strategyService)
        {
            _context = context;
            _strategyService = strategyService;
        }

        public async Task<DashboardOverviewDto> GetDashboardOverviewAsync()
        {
            var plants = await GetPlantsNeedingAttentionAsync();
            var careTasks = await GetUpcomingCareTasksAsync();
            var stats = await GetDashboardStatsAsync();

            return new DashboardOverviewDto
            {
                PlantsNeedingAttention = plants,
                UpcomingCareTasks = careTasks,
                Stats = stats
            };
        }

        public async Task<List<PlantNeedsAttentionDto>> GetPlantsNeedingAttentionAsync()
        {
            var plants = await _context.Plants
                .Include(p => p.CareEvents)
                .Where(p => p.HealthStatus == PlantHealthStatus.NeedsAttention)
                .ToListAsync();

            var result = new List<PlantNeedsAttentionDto>();

            foreach (var plant in plants)
            {
                var recommendation = _strategyService.GenerateRecommendation(plant);
                var nextCareDate = recommendation.NextWateringDate < recommendation.NextFertilizingDate
                    ? recommendation.NextWateringDate
                    : recommendation.NextFertilizingDate;

                result.Add(new PlantNeedsAttentionDto
                {
                    Id = plant.Id,
                    Name = plant.Name,
                    Species = plant.Species,
                    HealthStatus = plant.HealthStatus,
                    AttentionReason = DetermineAttentionReason(plant),
                    NextCareDate = nextCareDate,
                    CareType = DetermineCareType(recommendation),
                    StrategyName = recommendation.StrategyName
                });
            }

            return result;
        }
        public async Task<List<UpcomingCareDto>> GetUpcomingCareTasksAsync(int daysAhead = 7)
        {
            var endDate = DateTime.Today.AddDays(daysAhead);
            var plants = await _context.Plants
                .Include(p => p.CareEvents)
                .ToListAsync();

            var result = new List<UpcomingCareDto>();

            foreach (var plant in plants)
            {
                var recommendation = _strategyService.GenerateRecommendation(plant);

                // Watering
                if (recommendation.NextWateringDate <= endDate)
                {
                    result.Add(new UpcomingCareDto
                    {
                        PlantId = plant.Id,
                        PlantName = plant.Name,
                        CareType = "Watering",
                        DueDate = recommendation.NextWateringDate,
                        StrategyName = recommendation.StrategyName,
                        IsOverdue = recommendation.NextWateringDate < DateTime.Today
                    });
                }

                // Fertilizing
                if (recommendation.NextFertilizingDate.HasValue && recommendation.NextFertilizingDate.Value <= endDate)
                {
                    result.Add(new UpcomingCareDto
                    {
                        PlantId = plant.Id,
                        PlantName = plant.Name,
                        CareType = "Fertilizing",
                        DueDate = recommendation.NextFertilizingDate.Value,
                        StrategyName = recommendation.StrategyName,
                        IsOverdue = recommendation.NextFertilizingDate.Value < DateTime.Today
                    });
                }
            }

            return result.OrderBy(x => x.DueDate).ToList();
        }
        public async Task<DashboardStatsDto> GetDashboardStatsAsync()
        {
            var totalPlants = await _context.Plants.CountAsync();
            var healthyPlants = await _context.Plants
                .CountAsync(p => p.HealthStatus == PlantHealthStatus.Healthy);
            var plantsNeedingAttention = await _context.Plants
                .CountAsync(p => p.HealthStatus == PlantHealthStatus.NeedsAttention);

            var upcomingCareTasks = await GetUpcomingCareTasksAsync(7);

            return new DashboardStatsDto
            {
                TotalPlants = totalPlants,
                HealthyPlants = healthyPlants,
                PlantsNeedingAttention = plantsNeedingAttention,
                UpcomingCareTasks = upcomingCareTasks.Count
            };
        }

        private string DetermineAttentionReason(Plant plant)
        {
            if (plant.HealthStatus == PlantHealthStatus.NeedsAttention)
                return "Health issues detected";

            var recommendation = _strategyService.GenerateRecommendation(plant);
            if (recommendation.NextWateringDate < DateTime.Now)
                return "Overdue for watering";
            if (recommendation.NextFertilizingDate.HasValue &&
                recommendation.NextFertilizingDate.Value < DateTime.Now)
                return "Overdue for fertilizing";

            return "Regular care needed";
        }

        private string DetermineCareType(CareRecommendationDto recommendation)
        {
            if (recommendation.NextWateringDate < DateTime.Now)
                return "Watering";
            if (recommendation.NextFertilizingDate.HasValue &&
                recommendation.NextFertilizingDate.Value < DateTime.Now)
                return "Fertilizing";
            return "Regular Care";
        }
    }
}