using PlantCareBuddy.Application.DTOs.Plant;

namespace PlantCareBuddy.Application.Interfaces
{
    public interface IPlantService
    {
        Task<IEnumerable<PlantDto>> GetAllPlantsAsync();
        Task<PlantDto> CreatePlantAsync(CreatePlantDto dto);
    }
}