using REAL_ESTATE_CLEAN.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyDetailService
{
    private readonly AppDbContext _context;

    public PropertyDetailService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PropertyImage>> GetImages(Guid propertyId)
    {
        return await _context.PropertyImages
            .Where(x => x.PropertyListingId == propertyId)
            .ToListAsync();
    }
}
