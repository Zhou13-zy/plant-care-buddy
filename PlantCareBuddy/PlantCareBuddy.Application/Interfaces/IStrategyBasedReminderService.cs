using PlantCareBuddy.Domain.Entities;

namespace PlantCareBuddy.Application.Interfaces
{
    public interface IStrategyBasedReminderService
    {
        /// <summary>
        /// Generate a set of recommended reminders for a plant based on its care strategy.
        /// </summary>
        /// <param name="plant">The plant to generate reminders for.</param>
        /// <returns>A list of generated Reminder entities.</returns>
        Task<IEnumerable<Reminder>> GenerateRemindersForPlantAsync(Plant plant);
    }
} 