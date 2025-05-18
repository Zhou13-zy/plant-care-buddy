using PlantCareBuddy.Application.DTOs.HealthObservation;
using PlantCareBuddy.Infrastructure.Interfaces.Storage;

namespace PlantCareBuddy.Application.Interfaces
{
    public interface IHealthObservationService
    {
        Task<IEnumerable<HealthObservationDto>> GetAllHealthObservationsAsync();
        Task<IEnumerable<HealthObservationDto>> GetHealthObservationsByPlantIdAsync(int plantId);
        Task<HealthObservationDto> GetHealthObservationByIdAsync(int id);
        Task<HealthObservationDto> CreateHealthObservationAsync(CreateHealthObservationDto createDto, IPhotoStorageService photoStorage);
        Task<HealthObservationDto> UpdateHealthObservationAsync(int id, UpdateHealthObservationDto updateDto, IPhotoStorageService photoStorage);
        Task<bool> DeleteHealthObservationAsync(int id);
    }
}