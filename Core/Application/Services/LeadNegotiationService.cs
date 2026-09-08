using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadNegotiationService
{
    private readonly AppDbContext _context;

    public LeadNegotiationService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadNegotiation> CreateAsync(
        LeadNegotiation negotiation)
    {
        negotiation.CreatedAt = DateTime.UtcNow;
        negotiation.Status = "Open";

        _context.LeadNegotiations.Add(negotiation);

        await _context.SaveChangesAsync();

        return negotiation;
    }


    public async Task<List<LeadNegotiation>> GetByLeadAsync(
        int leadId)
    {
        return await _context.LeadNegotiations
            .Where(x => x.LeadId == leadId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<LeadNegotiation?> GetByIdAsync(
        int id)
    {
        return await _context.LeadNegotiations
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<bool> UpdateAsync(
        LeadNegotiation updated)
    {
        var negotiation = await _context.LeadNegotiations
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (negotiation == null)
            return false;


        negotiation.CurrentOffer = updated.CurrentOffer;
        negotiation.TargetPrice = updated.TargetPrice;
        negotiation.Status = updated.Status;
        negotiation.Notes = updated.Notes;
        negotiation.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> CloseAsync(
        int id,
        string status)
    {
        var negotiation = await _context.LeadNegotiations
            .FirstOrDefaultAsync(x => x.Id == id);

        if (negotiation == null)
            return false;


        negotiation.Status = status;
        
        negotiation.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteAsync(
        int id)
    {
        var negotiation = await _context.LeadNegotiations
            .FirstOrDefaultAsync(x => x.Id == id);

        if (negotiation == null)
            return false;


        _context.LeadNegotiations.Remove(negotiation);

        await _context.SaveChangesAsync();

        return true;
    }
}
