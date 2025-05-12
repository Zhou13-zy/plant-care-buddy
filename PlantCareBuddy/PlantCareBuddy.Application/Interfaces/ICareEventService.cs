using PlantCareBuddy.Application.DTOs.CareEvent;

namespace PlantCareBuddy.Application.Interfaces
{
    public interface ICareEventService
    {
        Task<IEnumerable<CareEventDto>> GetAllCareEventsAsync();
        Task<IEnumerable<CareEventDto>> GetCareEventsByPlantIdAsync(int plantId);
        Task<CareEventDto?> GetCareEventByIdAsync(int id);
    }
}