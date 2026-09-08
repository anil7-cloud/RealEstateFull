using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyViewService
{
    private readonly AppDbContext _context;

    public PropertyViewService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddView(int propertyId)
    {
        var property = await _context.Properties
            .FirstOrDefaultAsync(x => x.Id == propertyId);

        if (property == null)
            return;

        property.ViewCount++;

        await _context.SaveChangesAsync();
    }

    public async Task<int> GetViewCount(int propertyId)
    {
        var property = await _context.Properties
            .FirstOrDefaultAsync(x => x.Id == propertyId);

        return property?.ViewCount ?? 0;
    }

    public async Task<List<Property>> GetMostViewed(int count)
    {
        return await _context.Properties
            .OrderByDescending(x => x.ViewCount)
            .Take(count)
            .ToListAsync();
    }

    public async Task ResetViewCount(int propertyId)
    {
        var property = await _context.Properties
            .FirstOrDefaultAsync(x => x.Id == propertyId);

        if (property == null)
            return;

        property.ViewCount = 0;

        await _context.SaveChangesAsync();
    }
}
