using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadNotificationService
{
    private readonly AppDbContext _context;

    public LeadNotificationService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadNotification> CreateAsync(
        int leadId,
        string title,
        string message)
    {
        var notification = new LeadNotification
        {
            LeadId = leadId,
            Title = title,
            Message = message,
            IsRead = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadNotifications.Add(notification);

        await _context.SaveChangesAsync();

        return notification;
    }


    public async Task<List<LeadNotification>> GetByLeadAsync(
        int leadId)
    {
        return await _context.LeadNotifications
            .Where(x => x.LeadId == leadId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<bool> MarkAsReadAsync(
        int id)
    {
        var notification = await _context.LeadNotifications
            .FirstOrDefaultAsync(x => x.Id == id);

        if (notification == null)
            return false;

        notification.IsRead = true;

        await _context.SaveChangesAsync();

        return true;
    }
}
