using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadOfferService
{
    private readonly AppDbContext _context;

    public LeadOfferService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadOffer> CreateAsync(LeadOffer offer)
    {
        offer.CreatedAt = DateTime.UtcNow;
        offer.IsActive = true;

        _context.LeadOffers.Add(offer);

        await _context.SaveChangesAsync();

        return offer;
    }

    public async Task<List<LeadOffer>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadOffers
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderByDescending(x => x.OfferDate)
            .ToListAsync();
    }

    public async Task<LeadOffer?> GetByIdAsync(int id)
    {
        return await _context.LeadOffers
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateStatusAsync(int id, string status)
    {
        var offer = await _context.LeadOffers
            .FirstOrDefaultAsync(x => x.Id == id);

        if (offer is null)
            return false;

        offer.Status = status;
        offer.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateNotesAsync(int id, string notes)
    {
        var offer = await _context.LeadOffers
            .FirstOrDefaultAsync(x => x.Id == id);

        if (offer is null)
            return false;

        offer.Notes = notes;
        offer.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var offer = await _context.LeadOffers
            .FirstOrDefaultAsync(x => x.Id == id);

        if (offer is null)
            return false;

        offer.IsActive = false;
        offer.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
