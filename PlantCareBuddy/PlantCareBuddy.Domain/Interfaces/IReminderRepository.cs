using PlantCareBuddy.Domain.Entities;

public interface IReminderRepository
{
    Task<Reminder> GetByIdAsync(Guid id);
    Task<IEnumerable<Reminder>> GetByPlantIdAsync(Guid plantId);
    Task<IEnumerable<Reminder>> GetUpcomingAsync(DateTime from, DateTime to);
    Task<IEnumerable<Reminder>> GetAllAsync();
    Task AddAsync(Reminder reminder);
    Task UpdateAsync(Reminder reminder);
    Task DeleteAsync(Guid id);
}