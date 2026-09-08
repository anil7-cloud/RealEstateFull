using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyOfferService
{
    private readonly AppDbContext _context;

    public PropertyOfferService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<PropertyOffer> CreateOffer(PropertyOffer offer)
    {
        _context.PropertyOffers.Add(offer);

        await _context.SaveChangesAsync();

        return offer;
    }


    public async Task<List<PropertyOffer>> GetPropertyOffers(Guid propertyId)
    {
        return await _context.PropertyOffers
            .Where(x => x.PropertyListingId == propertyId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<List<PropertyOffer>> GetUserOffers(Guid userId)
    {
        return await _context.PropertyOffers
            .Where(x => x.CustomerUserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<bool> UpdateOfferStatus(Guid id, string status)
    {
        var offer = await _context.PropertyOffers
            .FirstOrDefaultAsync(x => x.Id == id);

        if (offer == null)
            return false;

        offer.Status = status.Trim();

        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteOffer(Guid id)
    {
        var offer = await _context.PropertyOffers
            .FirstOrDefaultAsync(x => x.Id == id);

        if (offer == null)
            return false;

        _context.PropertyOffers.Remove(offer);

        await _context.SaveChangesAsync();

        return true;
    }
}
