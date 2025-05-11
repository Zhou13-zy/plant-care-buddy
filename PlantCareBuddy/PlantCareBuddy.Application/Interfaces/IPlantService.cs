using PlantCareBuddy.Application.DTOs.Plant;
using PlantCareBuddy.Domain.Enums;

namespace PlantCareBuddy.Application.Interfaces
{
    public interface IPlantService
    {
        Task<IEnumerable<PlantDto>> GetAllPlantsAsync();
        Task<PlantDto?> GetPlantByIdAsync(int id);
        Task<PlantDto> CreatePlantAsync(CreatePlantDto dto);
        Task<IEnumerable<PlantDto>> CreatePlantsAsync(List<CreatePlantDto> dtos);
        Task<PlantDto?> UpdatePlantAsync(int id, UpdatePlantDto dto);
        Task<bool> DeletePlantAsync(int id);
        Task<IEnumerable<PlantDto>> SearchPlantsAsync(string? name, string? species, PlantHealthStatus? healthStatus, string? location);
    }
}