using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Application.DTOs.Statistics;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyStatisticsService
{
    private readonly AppDbContext _context;

    public PropertyStatisticsService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PropertyStatisticsDto>> GetUserStatistics(
        int userId)
    {
        return await _context.Properties
            .Where(x => x.AppUserId == userId)
            .Select(x => new PropertyStatisticsDto
            {
                PropertyId = x.Id,
                Title = x.Title,
                City = x.City,
                Price = x.Price,
                ViewCount = x.ViewCount,

                FavoriteCount = _context.Favorites
                    .Count(f => f.PropertyId == x.Id),

                MessageCount = 0
            })
            .OrderByDescending(x => x.TotalInteractions)
            .ToListAsync();
    }

    public async Task<int> GetTotalViews(int userId)
    {
        return await _context.Properties
            .Where(x => x.AppUserId == userId)
            .SumAsync(x => x.ViewCount);
    }

    public async Task<int> GetTotalFavorites(int userId)
    {
        var propertyIds = await _context.Properties
            .Where(x => x.AppUserId == userId)
            .Select(x => x.Id)
            .ToListAsync();

        if (propertyIds.Count == 0)
            return 0;

        return await _context.Favorites
            .CountAsync(x => propertyIds.Contains(x.PropertyId));
    }

    public async Task<int> GetTotalMessages(int userId)
    {
        return await _context.ChatMessages
            .CountAsync(x => x.ReceiverId == userId);
    }
}
