using Microsoft.EntityFrameworkCore;
using PlantCareBuddy.Domain.Entities;
using PlantCareBuddy.Domain.Interfaces;
using PlantCareBuddy.Infrastructure.Persistence;

public class ReminderRepository : IReminderRepository
{
    private readonly PlantCareBuddyContext _context;
    public ReminderRepository(PlantCareBuddyContext context) => _context = context;

    public async Task<Reminder> GetByIdAsync(Guid id) =>
        await _context.Reminders.Include(r => r.Plant).FirstOrDefaultAsync(r => r.Id == id);

    public async Task<IEnumerable<Reminder>> GetByPlantIdAsync(Guid plantId) =>
        await _context.Reminders.Where(r => r.PlantId == plantId).ToListAsync();

    public async Task<IEnumerable<Reminder>> GetUpcomingAsync(DateTime from, DateTime to) =>
        await _context.Reminders
            .Where(r => r.DueDate >= from && r.DueDate <= to && !r.IsCompleted)
            .ToListAsync();

    public async Task<IEnumerable<Reminder>> GetAllAsync() =>
        await _context.Reminders
            .Include(r => r.Plant)
            .OrderBy(r => r.DueDate)
            .ToListAsync();

    public async Task AddAsync(Reminder reminder)
    {
        _context.Reminders.Add(reminder);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Reminder reminder)
    {
        _context.Reminders.Update(reminder);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var reminder = await _context.Reminders.FindAsync(id);
        if (reminder != null)
        {
            _context.Reminders.Remove(reminder);
            await _context.SaveChangesAsync();
        }
    }
}