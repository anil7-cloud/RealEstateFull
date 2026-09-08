using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadOpportunityService
{
    private readonly AppDbContext _context;

    public LeadOpportunityService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadOpportunity> CreateAsync(LeadOpportunity opportunity)
    {
        opportunity.CreatedAt = DateTime.UtcNow;
        opportunity.IsActive = true;

        _context.LeadOpportunities.Add(opportunity);

        await _context.SaveChangesAsync();

        return opportunity;
    }

    public async Task<List<LeadOpportunity>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadOpportunities
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderByDescending(x => x.EstimatedValue)
            .ThenByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<LeadOpportunity?> GetByIdAsync(int id)
    {
        return await _context.LeadOpportunities
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateAsync(LeadOpportunity updated)
    {
        var opportunity = await _context.LeadOpportunities
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (opportunity is null)
            return false;

        opportunity.PropertyId = updated.PropertyId;
        opportunity.OpportunityName = updated.OpportunityName;
        opportunity.Stage = updated.Stage;
        opportunity.EstimatedValue = updated.EstimatedValue;
        opportunity.Probability = updated.Probability;
        opportunity.ExpectedCloseDate = updated.ExpectedCloseDate;
        opportunity.Source = updated.Source;
        opportunity.Status = updated.Status;
        opportunity.Notes = updated.Notes;
        opportunity.IsWon = updated.IsWon;
        opportunity.IsLost = updated.IsLost;
        opportunity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var opportunity = await _context.LeadOpportunities
            .FirstOrDefaultAsync(x => x.Id == id);

        if (opportunity is null)
            return false;

        opportunity.IsActive = false;
        opportunity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
