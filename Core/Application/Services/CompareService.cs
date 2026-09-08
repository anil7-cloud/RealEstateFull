using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class CompareService
{
    private readonly AppDbContext _context;

    public CompareService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddToCompare(int userId, int propertyId)
    {
        var exists = await _context.CompareItems
            .AnyAsync(x => x.UserId == userId && x.PropertyId == propertyId);

        if (exists)
            return;

        _context.CompareItems.Add(new CompareItem
        {
            UserId = userId,
            PropertyId = propertyId
        });

        await _context.SaveChangesAsync();
    }

    public async Task<List<CompareItem>> GetCompareList(int userId)
    {
        return await _context.CompareItems
            .Where(x => x.UserId == userId)
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    public async Task RemoveFromCompare(int userId, int propertyId)
    {
        var item = await _context.CompareItems
            .FirstOrDefaultAsync(x =>
                x.UserId == userId &&
                x.PropertyId == propertyId);

        if (item == null)
            return;

        _context.CompareItems.Remove(item);

        await _context.SaveChangesAsync();
    }
}
