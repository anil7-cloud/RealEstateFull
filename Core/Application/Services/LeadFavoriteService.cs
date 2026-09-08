using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadFavoriteService
{
    private readonly AppDbContext _context;

    public LeadFavoriteService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadFavorite> CreateAsync(LeadFavorite favorite)
    {
        favorite.CreatedAt = DateTime.UtcNow;
        favorite.FavoritedAt = DateTime.UtcNow;
        favorite.IsActive = true;

        _context.LeadFavorites.Add(favorite);

        await _context.SaveChangesAsync();

        return favorite;
    }

    public async Task<List<LeadFavorite>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadFavorites
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderByDescending(x => x.Priority)
            .ThenByDescending(x => x.FavoritedAt)
            .ToListAsync();
    }

    public async Task<LeadFavorite?> GetByIdAsync(int id)
    {
        return await _context.LeadFavorites
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateAsync(LeadFavorite updated)
    {
        var favorite = await _context.LeadFavorites
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (favorite is null)
            return false;

        favorite.Priority = updated.Priority;
        favorite.Notes = updated.Notes;
        favorite.IsViewed = updated.IsViewed;
        favorite.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var favorite = await _context.LeadFavorites
            .FirstOrDefaultAsync(x => x.Id == id);

        if (favorite is null)
            return false;

        favorite.IsActive = false;
        favorite.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
