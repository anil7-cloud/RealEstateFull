using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadPushEventService
{
    private readonly AppDbContext _context;

    public LeadPushEventService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadPushEvent> CreateAsync(LeadPushEvent pushEvent)
    {
        pushEvent.CreatedAt = DateTime.UtcNow;

        _context.LeadPushEvents.Add(pushEvent);

        await _context.SaveChangesAsync();

        return pushEvent;
    }

    public async Task<List<LeadPushEvent>> GetAllAsync()
    {
        return await _context.LeadPushEvents
            .OrderByDescending(x => x.EventTime)
            .ToListAsync();
    }

    public async Task<LeadPushEvent?> GetByIdAsync(int id)
    {
        return await _context.LeadPushEvents
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<LeadPushEvent>> GetByCampaignAsync(int campaignId)
    {
        return await _context.LeadPushEvents
            .Where(x => x.LeadPushCampaignId == campaignId)
            .OrderByDescending(x => x.EventTime)
            .ToListAsync();
    }

    public async Task<List<LeadPushEvent>> GetByRecipientAsync(int recipientId)
    {
        return await _context.LeadPushEvents
            .Where(x => x.LeadPushCampaignRecipientId == recipientId)
            .OrderByDescending(x => x.EventTime)
            .ToListAsync();
    }

    public async Task<List<LeadPushEvent>> GetByUserAsync(int userId)
    {
        return await _context.LeadPushEvents
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.EventTime)
            .ToListAsync();
    }

    public async Task<List<LeadPushEvent>> GetByEventTypeAsync(string eventType)
    {
        return await _context.LeadPushEvents
            .Where(x => x.EventType == eventType)
            .OrderByDescending(x => x.EventTime)
            .ToListAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var pushEvent = await _context.LeadPushEvents
            .FirstOrDefaultAsync(x => x.Id == id);

        if (pushEvent == null)
            return false;

        _context.LeadPushEvents.Remove(pushEvent);

        await _context.SaveChangesAsync();

        return true;
    }
}
