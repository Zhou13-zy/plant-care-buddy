using PlantCareBuddy.Application.DTOs.Plant;
using PlantCareBuddy.Domain.Enums;
using PlantCareBuddy.Infrastructure.Interfaces.Storage;

namespace PlantCareBuddy.Application.Interfaces
{
    public interface IPlantService
    {
        Task<IEnumerable<PlantDto>> GetAllPlantsAsync();
        Task<PlantDto?> GetPlantByIdAsync(Guid id);
        Task<PlantDto> CreatePlantAsync(CreatePlantDto dto, IPhotoStorageService photoStorage);
        Task<IEnumerable<PlantDto>> CreatePlantsAsync(List<CreatePlantDto> dtos);
        Task<PlantDto?> UpdatePlantAsync(Guid id, UpdatePlantDto dto, IPhotoStorageService photoStorage);
        Task<bool> DeletePlantAsync(Guid id);
        Task<IEnumerable<PlantDto>> SearchPlantsAsync(string? name, string? species, PlantHealthStatus? healthStatus, string? location);
    }
}