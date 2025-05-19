namespace PlantCareBuddy.Application.DTOs.Care
{
    /// <summary>
    /// DTO for plant care recommendations based on strategy calculations
    /// </summary>
    public class CareRecommendationDto
    {
        public int PlantId { get; set; }

        public string PlantName { get; set; }

        public string StrategyName { get; set; }

        public string StrategyDescription { get; set; }

        /// <summary>
        /// Recommended watering interval in days
        /// </summary>
        public int WateringIntervalDays { get; set; }

        /// <summary>
        /// Next recommended watering date
        /// </summary>
        public DateTime NextWateringDate { get; set; }

        /// <summary>
        /// Recommended fertilizing interval in days (0 means don't fertilize)
        /// </summary>
        public int FertilizingIntervalDays { get; set; }

        /// <summary>
        /// Next recommended fertilizing date (null if no fertilizing needed)
        /// </summary>
        public DateTime? NextFertilizingDate { get; set; }

        /// <summary>
        /// Light recommendation text
        /// </summary>
        public string LightRecommendation { get; set; }

        /// <summary>
        /// Humidity recommendation text
        /// </summary>
        public string HumidityRecommendation { get; set; }

        /// <summary>
        /// The current season used for calculations
        /// </summary>
        public string CurrentSeason { get; set; }
    }
}