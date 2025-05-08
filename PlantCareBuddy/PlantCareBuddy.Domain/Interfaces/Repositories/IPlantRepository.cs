using PlantCareBuddy.Domain.Entities;

namespace PlantCareBuddy.Domain.Interfaces.Repositories
{
    public interface IPlantRepository
    {
        Task<Plant> GetByIdAsync(int id);
        Task<IEnumerable<Plant>> GetAllAsync();
        Task<Plant> AddAsync(Plant plant);
        Task UpdateAsync(Plant plant);
        Task DeleteAsync(int id);
    }
}
