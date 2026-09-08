using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertySubscriptionService
{
    private readonly AppDbContext _context;

    public PropertySubscriptionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PropertySubscription> Subscribe(
        int userId,
        int propertyId)
    {
        var exists = await _context.PropertySubscriptions
            .AnyAsync(x =>
                x.UserId == userId &&
                x.PropertyId == propertyId);

        if (exists)
            throw new InvalidOperationException(
                "Kullanıcı zaten bu ilana abone.");

        var subscription = new PropertySubscription
        {
            UserId = userId,
            PropertyId = propertyId,
            CreatedAt = DateTime.UtcNow
        };

        _context.PropertySubscriptions.Add(subscription);

        await _context.SaveChangesAsync();

        return subscription;
    }

    public async Task<List<PropertySubscription>>
        GetUserSubscriptions(int userId)
    {
        return await _context.PropertySubscriptions
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<bool> UpdateNotificationSettings(
        int id,
        bool notifyPriceChanges,
        bool notifyStatusChanges,
        bool notifyNewPhotos,
        bool notifyOpenHouse)
    {
        var subscription = await _context.PropertySubscriptions
            .FirstOrDefaultAsync(x => x.Id == id);

        if (subscription is null)
            return false;

        subscription.NotifyPriceChanges = notifyPriceChanges;
        subscription.NotifyStatusChanges = notifyStatusChanges;
        subscription.NotifyNewPhotos = notifyNewPhotos;
        subscription.NotifyOpenHouse = notifyOpenHouse;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Unsubscribe(int id)
    {
        var subscription = await _context.PropertySubscriptions
            .FirstOrDefaultAsync(x => x.Id == id);

        if (subscription is null)
            return false;

        _context.PropertySubscriptions.Remove(subscription);

        await _context.SaveChangesAsync();

        return true;
    }
}
