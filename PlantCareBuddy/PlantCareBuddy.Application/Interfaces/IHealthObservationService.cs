using PlantCareBuddy.Application.DTOs.HealthObservation;
using PlantCareBuddy.Infrastructure.Interfaces.Storage;

namespace PlantCareBuddy.Application.Interfaces
{
    public interface IHealthObservationService
    {
        Task<IEnumerable<HealthObservationDto>> GetAllHealthObservationsAsync();
        Task<IEnumerable<HealthObservationDto>> GetHealthObservationsByPlantIdAsync(Guid plantId);
        Task<HealthObservationDto> GetHealthObservationByIdAsync(Guid id);
        Task<HealthObservationDto> CreateHealthObservationAsync(CreateHealthObservationDto createDto, IPhotoStorageService photoStorage);
        Task<HealthObservationDto> UpdateHealthObservationAsync(Guid id, UpdateHealthObservationDto updateDto, IPhotoStorageService photoStorage);
        Task<bool> DeleteHealthObservationAsync(Guid id);
    }
}