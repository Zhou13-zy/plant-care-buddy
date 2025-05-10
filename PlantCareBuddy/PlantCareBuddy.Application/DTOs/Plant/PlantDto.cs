namespace PlantCareBuddy.Application.DTOs.Plant
{
    /// <summary>
    /// Data Transfer Object for Plant entity.
    /// </summary>
    public class PlantDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public string Location { get; set; }
        public string HealthStatus { get; set; }
    }
}