using REAL_ESTATE_CLEAN.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace REAL_ESTATE_CLEAN.Core.Application;

public class AnalyticsService
{
    private readonly AppDbContext _context;

    public AnalyticsService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetUserCount()
    {
        return await _context.Users.CountAsync();
    }

    public async Task<int> GetViewCount()
    {
        return await _context.PropertyViews.CountAsync();
    }
}
