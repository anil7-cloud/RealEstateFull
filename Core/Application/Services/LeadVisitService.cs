using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadVisitService
{
    private readonly AppDbContext _context;

    public LeadVisitService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadVisit> CreateAsync(LeadVisit visit)
    {
        visit.CreatedAt = DateTime.UtcNow;
        visit.IsActive = true;

        _context.LeadVisits.Add(visit);

        await _context.SaveChangesAsync();

        return visit;
    }

    public async Task<List<LeadVisit>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadVisits
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderByDescending(x => x.VisitDate)
            .ToListAsync();
    }

    public async Task<LeadVisit?> GetByIdAsync(int id)
    {
        return await _context.LeadVisits
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateAsync(LeadVisit updated)
    {
        var visit = await _context.LeadVisits
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (visit is null)
            return false;

        visit.VisitDate = updated.VisitDate;
        visit.Status = updated.Status;
        visit.Rating = updated.Rating;
        visit.Interested = updated.Interested;
        visit.WantsSecondVisit = updated.WantsSecondVisit;
        visit.Feedback = updated.Feedback;
        visit.Notes = updated.Notes;
        visit.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> CompleteAsync(int id)
    {
        var visit = await _context.LeadVisits
            .FirstOrDefaultAsync(x => x.Id == id);

        if (visit is null)
            return false;

        visit.Status = "Completed";
        visit.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> CancelAsync(int id)
    {
        var visit = await _context.LeadVisits
            .FirstOrDefaultAsync(x => x.Id == id);

        if (visit is null)
            return false;

        visit.Status = "Cancelled";
        visit.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var visit = await _context.LeadVisits
            .FirstOrDefaultAsync(x => x.Id == id);

        if (visit is null)
            return false;

        visit.IsActive = false;
        visit.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
