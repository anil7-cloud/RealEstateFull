using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class RecentlyViewedService
{
    private readonly AppDbContext _context;

    public RecentlyViewedService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddView(int userId, int propertyId)
    {
        var view = new RecentlyViewed
        {
            UserId = userId,
            PropertyId = propertyId,
            ViewedAt = DateTime.UtcNow
        };

        _context.RecentlyVieweds.Add(view);

        await _context.SaveChangesAsync();
    }

    public async Task<List<RecentlyViewed>> GetRecentViews(int userId)
    {
        return await _context.RecentlyVieweds
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.ViewedAt)
            .Take(20)
            .ToListAsync();
    }

    public async Task ClearRecentViews(int userId)
    {
        var items = await _context.RecentlyVieweds
            .Where(x => x.UserId == userId)
            .ToListAsync();

        _context.RecentlyVieweds.RemoveRange(items);

        await _context.SaveChangesAsync();
    }
}
