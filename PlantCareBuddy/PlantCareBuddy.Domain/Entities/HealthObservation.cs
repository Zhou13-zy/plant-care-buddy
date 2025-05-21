using PlantCareBuddy.Domain.Enums;

namespace PlantCareBuddy.Domain.Entities
{
    public class HealthObservation
    {
        public Guid Id { get; set; }
        public Guid PlantId { get; set; }
        public DateTime ObservationDate { get; set; }
        public PlantHealthStatus HealthStatus { get; set; }
        public string Notes { get; set; }
        public string? ImagePath { get; set; }


        public virtual Plant Plant { get; set; }
    }
}