using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadPushSubscriptionService
{
    private readonly AppDbContext _context;

    public LeadPushSubscriptionService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadPushSubscription> CreateAsync(
        int topicId,
        string deviceToken,
        string platform)
    {
        var subscription = new LeadPushSubscription
        {
            LeadPushTopicId = topicId,
            DeviceToken = deviceToken,
            Platform = platform,
            IsSubscribed = true,
            SubscribedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadPushSubscriptions.Add(subscription);

        await _context.SaveChangesAsync();

        return subscription;
    }


    public async Task<List<LeadPushSubscription>> GetActiveAsync()
    {
        return await _context.LeadPushSubscriptions
            .Where(x => x.IsSubscribed)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<bool> DisableAsync(int id)
    {
        var subscription = await _context.LeadPushSubscriptions
            .FirstOrDefaultAsync(x => x.Id == id);

        if (subscription == null)
            return false;

        subscription.IsSubscribed = false;
        subscription.UnsubscribedAt = DateTime.UtcNow;
        subscription.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
