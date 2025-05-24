using PlantCareBuddy.Domain.Enums;

namespace PlantCareBuddy.Application.DTOs.Dashboard
{
    public class PlantNeedsAttentionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public PlantHealthStatus HealthStatus { get; set; }
        public string AttentionReason { get; set; }
        public DateTime? NextCareDate { get; set; }
        public string CareType { get; set; }
        public string StrategyName { get; set; }
    }
}