using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyAmenityService
{
    private readonly AppDbContext _context;

    public PropertyAmenityService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<PropertyAmenity> CreateAmenity(PropertyAmenity amenity)
    {
        _context.PropertyAmenities.Add(amenity);

        await _context.SaveChangesAsync();

        return amenity;
    }


    public async Task<List<PropertyAmenity>> GetPropertyAmenities(Guid propertyId)
    {
        return await _context.PropertyAmenities
            .Where(x => x.PropertyListingId == propertyId)
            .OrderBy(x => x.AmenityName)
            .ToListAsync();
    }


    public async Task<bool> DeleteAmenity(Guid id)
    {
        var amenity = await _context.PropertyAmenities
            .FirstOrDefaultAsync(x => x.Id == id);

        if (amenity == null)
            return false;

        _context.PropertyAmenities.Remove(amenity);

        await _context.SaveChangesAsync();

        return true;
    }
}
