using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadReminderService
{
    private readonly AppDbContext _context;

    public LeadReminderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadReminder> CreateAsync(
        LeadReminder reminder)
    {
        reminder.CreatedAt = DateTime.UtcNow;

        _context.LeadReminders.Add(reminder);

        await _context.SaveChangesAsync();

        return reminder;
    }

    public async Task<List<LeadReminder>> GetByLeadAsync(
        int leadId)
    {
        return await _context.LeadReminders
            .Where(x =>
                x.LeadId == leadId &&
                !x.IsCancelled)
            .OrderBy(x => x.ReminderDate)
            .ToListAsync();
    }

    public async Task<List<LeadReminder>> GetUpcomingAsync()
    {
        return await _context.LeadReminders
            .Where(x =>
                !x.IsCancelled &&
                !x.IsCompleted &&
                x.ReminderDate >= DateTime.UtcNow)
            .OrderBy(x => x.ReminderDate)
            .ToListAsync();
    }

    public async Task<LeadReminder?> GetByIdAsync(int id)
    {
        return await _context.LeadReminders
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> CompleteAsync(int id)
    {
        var reminder = await _context.LeadReminders
            .FirstOrDefaultAsync(x => x.Id == id);

        if (reminder is null)
            return false;

        reminder.IsCompleted = true;
        reminder.CompletedAt = DateTime.UtcNow;
        reminder.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> CancelAsync(int id)
    {
        var reminder = await _context.LeadReminders
            .FirstOrDefaultAsync(x => x.Id == id);

        if (reminder is null)
            return false;

        reminder.IsCancelled = true;
        reminder.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var reminder = await _context.LeadReminders
            .FirstOrDefaultAsync(x => x.Id == id);

        if (reminder is null)
            return false;

        _context.LeadReminders.Remove(reminder);

        await _context.SaveChangesAsync();

        return true;
    }
}
