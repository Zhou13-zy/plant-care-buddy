using PlantCareBuddy.Application.DTOs.CareEvent;
using PlantCareBuddy.Infrastructure.Interfaces.Storage;

namespace PlantCareBuddy.Application.Interfaces
{
    public interface ICareEventService
    {
        Task<IEnumerable<CareEventDto>> GetAllCareEventsAsync();
        Task<IEnumerable<CareEventDto>> GetCareEventsByPlantIdAsync(Guid plantId);
        Task<CareEventDto?> GetCareEventByIdAsync(Guid id);
        Task<CareEventDto> CreateCareEventAsync(CreateCareEventDto dto, IPhotoStorageService photoStorage);
        Task<CareEventDto?> UpdateCareEventAsync(Guid id, UpdateCareEventDto dto, IPhotoStorageService photoStorage);
        Task<bool> DeleteCareEventAsync(Guid id);
    }
}