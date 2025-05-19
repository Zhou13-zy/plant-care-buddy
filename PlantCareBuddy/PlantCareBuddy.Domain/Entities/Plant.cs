using PlantCareBuddy.Domain.Enums;

namespace PlantCareBuddy.Domain.Entities
{
    public class Plant
    {
        public Plant()
        {
            CareEvents = new HashSet<CareEvent>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public PlantType PlantType { get; set; } = PlantType.Default;
        public DateTime AcquisitionDate { get; set; }
        public string Location { get; set; }
        public PlantHealthStatus HealthStatus { get; set; }
        public DateTime? NextHealthCheckDate { get; set; }
        public string? Notes { get; set; }
        public string? PrimaryImagePath { get; set; }

        // Navigation property - collection of care events
        public virtual ICollection<CareEvent> CareEvents { get; set; }
        public virtual ICollection<HealthObservation> HealthObservations { get; set; }
    }
}