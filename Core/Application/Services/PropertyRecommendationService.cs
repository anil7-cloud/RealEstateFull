using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyRecommendationService
{
    private readonly AppDbContext _context;

    public PropertyRecommendationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Property>> GetRecommendedProperties(
        decimal minPrice,
        decimal maxPrice,
        string city,
        int count)
    {
        return await _context.Properties
            .Where(x =>
                x.IsActive &&
                x.Price >= minPrice &&
                x.Price <= maxPrice &&
                x.Location.Contains(city))
            .OrderByDescending(x => x.CreatedAt)
            .Take(count)
            .ToListAsync();
    }

    public async Task<List<Property>> GetSimilarProperties(
        int propertyId,
        int count)
    {
        var property = await _context.Properties
            .FirstOrDefaultAsync(x => x.Id == propertyId);

        if (property == null)
            return new List<Property>();

        return await _context.Properties
            .Where(x =>
                x.Id != property.Id &&
                x.Location == property.Location &&
                x.Price >= property.Price * 0.8m &&
                x.Price <= property.Price * 1.2m)
            .Take(count)
            .ToListAsync();
    }
}
