using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyComparisonService
{
    private readonly AppDbContext _context;

    public PropertyComparisonService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PropertyComparison> AddProperty(
        int userId,
        int propertyId)
    {
        var exists = await _context.PropertyComparisons
            .AnyAsync(x =>
                x.UserId == userId &&
                x.PropertyId == propertyId);

        if (exists)
            throw new InvalidOperationException(
                "İlan zaten karşılaştırma listesinde.");

        var order =
            await _context.PropertyComparisons
                .CountAsync(x => x.UserId == userId);

        var comparison = new PropertyComparison
        {
            UserId = userId,
            PropertyId = propertyId,
            DisplayOrder = order + 1,
            CreatedAt = DateTime.UtcNow
        };

        _context.PropertyComparisons.Add(comparison);

        await _context.SaveChangesAsync();

        return comparison;
    }

    public async Task<List<PropertyComparison>>
        GetUserComparisons(int userId)
    {
        return await _context.PropertyComparisons
            .Where(x => x.UserId == userId)
            .OrderBy(x => x.DisplayOrder)
            .ToListAsync();
    }

    public async Task<bool> RemoveProperty(
        int userId,
        int propertyId)
    {
        var item = await _context.PropertyComparisons
            .FirstOrDefaultAsync(x =>
                x.UserId == userId &&
                x.PropertyId == propertyId);

        if (item is null)
            return false;

        _context.PropertyComparisons.Remove(item);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ClearComparison(
        int userId)
    {
        var items = await _context.PropertyComparisons
            .Where(x => x.UserId == userId)
            .ToListAsync();

        if (!items.Any())
            return false;

        _context.PropertyComparisons.RemoveRange(items);

        await _context.SaveChangesAsync();

        return true;
    }
}
