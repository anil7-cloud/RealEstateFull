using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class FavoriteSearchService
{
    private readonly AppDbContext _context;

    public FavoriteSearchService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FavoriteSearch> CreateSearch(
        FavoriteSearch search)
    {
        _context.FavoriteSearches.Add(search);

        await _context.SaveChangesAsync();

        return search;
    }

    public async Task<List<FavoriteSearch>>
        GetUserSearches(int userId)
    {
        return await _context.FavoriteSearches
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<FavoriteSearch?> GetSearch(int id)
    {
        return await _context.FavoriteSearches
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> DeleteSearch(int id)
    {
        var search = await _context.FavoriteSearches
            .FirstOrDefaultAsync(x => x.Id == id);

        if (search is null)
            return false;

        _context.FavoriteSearches.Remove(search);

        await _context.SaveChangesAsync();

        return true;
    }
}
