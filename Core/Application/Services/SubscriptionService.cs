using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class SubscriptionService
{
    private readonly AppDbContext _context;

    public SubscriptionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Subscription> CreateSubscription(
        int userId,
        string planName,
        decimal price,
        int durationDays)
    {
        var subscription = new Subscription
        {
            UserId = userId,
            PlanName = planName,
            Price = price,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(durationDays),
            IsActive = true
        };

        _context.Subscriptions.Add(subscription);

        await _context.SaveChangesAsync();

        return subscription;
    }


    public async Task<Subscription?> GetSubscriptionById(int id)
    {
        return await _context.Subscriptions
            .FirstOrDefaultAsync(x => x.Id == id);
    }

}
