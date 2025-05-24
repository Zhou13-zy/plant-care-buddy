using PlantCareBuddy.Application.DTOs.Reminder;

namespace PlantCareBuddy.Application.Interfaces
{
    public interface IReminderService
    {
        Task<ReminderDto> CreateReminderAsync(CreateReminderDto dto);
        Task<ReminderDto?> GetReminderByIdAsync(Guid id);
        Task<IEnumerable<ReminderDto>> GetRemindersByPlantIdAsync(Guid plantId);
        Task<ReminderDto?> UpdateReminderAsync(Guid id, UpdateReminderDto dto);
        Task<bool> DeleteReminderAsync(Guid id);
        Task<ReminderDto?> MarkAsCompletedAsync(Guid id);
    }
}