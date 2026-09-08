using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadLostReasonService
{
    private readonly AppDbContext _context;

    public LeadLostReasonService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadLostReason> CreateAsync(
        LeadLostReason reason)
    {
        reason.CreatedAt = DateTime.UtcNow;
        reason.IsActive = true;

        _context.LeadLostReasons.Add(reason);

        await _context.SaveChangesAsync();

        return reason;
    }


    public async Task<List<LeadLostReason>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadLostReasons
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<bool> UpdateAsync(
        LeadLostReason updated)
    {
        var reason = await _context.LeadLostReasons
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (reason == null)
            return false;


        reason.ReasonCategory = updated.ReasonCategory;
        reason.Reason = updated.Reason;
        reason.CompetitorName = updated.CompetitorName;
        reason.CompetitorPrice = updated.CompetitorPrice;
        reason.CanBeRecovered = updated.CanBeRecovered;
        reason.RecoveryPlan = updated.RecoveryPlan;
        reason.Notes = updated.Notes;
        reason.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var reason = await _context.LeadLostReasons
            .FirstOrDefaultAsync(x => x.Id == id);

        if(reason == null)
            return false;


        reason.IsActive = false;
        reason.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
