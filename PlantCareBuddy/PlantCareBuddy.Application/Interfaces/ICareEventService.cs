using PlantCareBuddy.Application.DTOs.CareEvent;
using PlantCareBuddy.Infrastructure.Interfaces.Storage;

namespace PlantCareBuddy.Application.Interfaces
{
    public interface ICareEventService
    {
        Task<IEnumerable<CareEventDto>> GetAllCareEventsAsync();
        Task<IEnumerable<CareEventDto>> GetCareEventsByPlantIdAsync(int plantId);
        Task<CareEventDto?> GetCareEventByIdAsync(int id);
        Task<CareEventDto> CreateCareEventAsync(CreateCareEventDto dto, IPhotoStorageService photoStorage);
        Task<CareEventDto?> UpdateCareEventAsync(int id, UpdateCareEventDto dto, IPhotoStorageService photoStorage);
        Task<bool> DeleteCareEventAsync(int id);
    }
}