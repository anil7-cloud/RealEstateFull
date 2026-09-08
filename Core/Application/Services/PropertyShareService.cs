using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyShareService
{
    private readonly AppDbContext _context;

    public PropertyShareService(AppDbContext context)
    {
        _context = context;
    }

    public async Task ShareProperty(
        int propertyId,
        int userId,
        string platform)
    {
        var share = new PropertyShare
        {
            PropertyId = propertyId,
            UserId = userId,
            Platform = platform,
            SharedAt = DateTime.UtcNow
        };

        _context.PropertyShares.Add(share);

        await _context.SaveChangesAsync();
    }

    public async Task<List<PropertyShare>> GetShares(int propertyId)
    {
        return await _context.PropertyShares
            .Where(x => x.PropertyId == propertyId)
            .OrderByDescending(x => x.SharedAt)
            .ToListAsync();
    }

    public async Task<int> GetShareCount(int propertyId)
    {
        return await _context.PropertyShares
            .CountAsync(x => x.PropertyId == propertyId);
    }

    public async Task<List<PropertyShare>> GetUserShares(int userId)
    {
        return await _context.PropertyShares
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.SharedAt)
            .ToListAsync();
    }
}
