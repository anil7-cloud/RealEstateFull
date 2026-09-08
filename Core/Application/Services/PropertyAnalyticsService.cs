using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyAnalyticsService
{
    private readonly AppDbContext _context;

    public PropertyAnalyticsService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetTotalProperties()
    {
        return await _context.Properties.CountAsync();
    }

    public async Task<int> GetActiveProperties()
    {
        return await _context.Properties
            .CountAsync(x => x.IsActive);
    }

    public async Task<int> GetInactiveProperties()
    {
        return await _context.Properties
            .CountAsync(x => !x.IsActive);
    }

    public async Task<decimal> GetAveragePrice()
    {
        if (!await _context.Properties.AnyAsync())
            return 0;

        return await _context.Properties
            .AverageAsync(x => x.Price);
    }

    public async Task<Property?> GetMostExpensiveProperty()
    {
        return await _context.Properties
            .OrderByDescending(x => x.Price)
            .FirstOrDefaultAsync();
    }

    public async Task<Property?> GetCheapestProperty()
    {
        return await _context.Properties
            .OrderBy(x => x.Price)
            .FirstOrDefaultAsync();
    }
}
