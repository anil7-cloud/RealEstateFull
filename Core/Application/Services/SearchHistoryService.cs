using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class SearchHistoryService
{
    private readonly AppDbContext _context;

    public SearchHistoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddSearch(int userId, string keyword)
    {
        var history = new SearchHistory
        {
            UserId = userId,
            Keyword = keyword,
            CreatedAt = DateTime.UtcNow
        };

        _context.SearchHistories.Add(history);

        await _context.SaveChangesAsync();
    }

    public async Task<List<SearchHistory>> GetHistory(int userId)
    {
        return await _context.SearchHistories
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task ClearHistory(int userId)
    {
        var history = await _context.SearchHistories
            .Where(x => x.UserId == userId)
            .ToListAsync();

        _context.SearchHistories.RemoveRange(history);

        await _context.SaveChangesAsync();
    }
}
