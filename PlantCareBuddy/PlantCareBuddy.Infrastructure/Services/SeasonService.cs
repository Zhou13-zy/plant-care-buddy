using PlantCareBuddy.Domain.Enums;
using PlantCareBuddy.Domain.Interfaces;

namespace PlantCareBuddy.Infrastructure.Services
{
    public class SeasonService : ISeasonService
    {
        public Season GetCurrentSeason()
        {
            var month = DateTime.Now.Month;

            // Southern Hemisphere seasons (Australia)
            return month switch
            {
                >= 12 or <= 2 => Season.Summer,
                >= 3 and <= 5 => Season.Autumn,
                >= 6 and <= 8 => Season.Winter,
                _ => Season.Spring // 9-11
            };
        }
    }
}