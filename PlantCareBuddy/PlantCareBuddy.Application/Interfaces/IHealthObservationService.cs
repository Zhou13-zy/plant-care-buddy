using PlantCareBuddy.Application.DTOs.HealthObservation;

namespace PlantCareBuddy.Application.Interfaces
{
    public interface IHealthObservationService
    {
        Task<IEnumerable<HealthObservationDto>> GetAllHealthObservationsAsync();
        Task<IEnumerable<HealthObservationDto>> GetHealthObservationsByPlantIdAsync(int plantId);
        Task<HealthObservationDto> GetHealthObservationByIdAsync(int id);
        Task<HealthObservationDto> CreateHealthObservationAsync(CreateHealthObservationDto createDto);
        Task<HealthObservationDto> UpdateHealthObservationAsync(int id, UpdateHealthObservationDto updateDto);
        Task<bool> DeleteHealthObservationAsync(int id);
    }
}