using PlantCareBuddy.Domain.Enums;

namespace PlantCareBuddy.Domain.Interfaces
{
    public interface ISeasonService
    {
        Season GetCurrentSeason();
    }
}