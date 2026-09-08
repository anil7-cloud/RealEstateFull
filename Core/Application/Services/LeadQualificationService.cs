using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadQualificationService
{
    private readonly AppDbContext _context;

    public LeadQualificationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadQualification> CreateAsync(LeadQualification qualification)
    {
        qualification.CreatedAt = DateTime.UtcNow;
        qualification.QualifiedAt = DateTime.UtcNow;
        qualification.IsActive = true;

        _context.LeadQualifications.Add(qualification);

        await _context.SaveChangesAsync();

        return qualification;
    }

    public async Task<List<LeadQualification>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadQualifications
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderByDescending(x => x.QualifiedAt)
            .ToListAsync();
    }

    public async Task<LeadQualification?> GetByIdAsync(int id)
    {
        return await _context.LeadQualifications
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateAsync(LeadQualification updated)
    {
        var qualification = await _context.LeadQualifications
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (qualification is null)
            return false;

        qualification.UserId = updated.UserId;
        qualification.BudgetScore = updated.BudgetScore;
        qualification.InterestScore = updated.InterestScore;
        qualification.UrgencyScore = updated.UrgencyScore;
        qualification.AuthorityScore = updated.AuthorityScore;
        qualification.NeedScore = updated.NeedScore;
        qualification.TimelineScore = updated.TimelineScore;
        qualification.TotalScore = updated.TotalScore;
        qualification.QualificationLevel = updated.QualificationLevel;
        qualification.IsQualified = updated.IsQualified;
        qualification.Notes = updated.Notes;
        qualification.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var qualification = await _context.LeadQualifications
            .FirstOrDefaultAsync(x => x.Id == id);

        if (qualification is null)
            return false;

        qualification.IsActive = false;
        qualification.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
