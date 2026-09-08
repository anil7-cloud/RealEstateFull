using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadRiskAssessmentService
{
    private readonly AppDbContext _context;

    public LeadRiskAssessmentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadRiskAssessment> CreateAsync(LeadRiskAssessment assessment)
    {
        assessment.CreatedAt = DateTime.UtcNow;
        assessment.IsActive = true;

        _context.LeadRiskAssessments.Add(assessment);

        await _context.SaveChangesAsync();

        return assessment;
    }

    public async Task<List<LeadRiskAssessment>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadRiskAssessments
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderByDescending(x => x.RiskScore)
            .ToListAsync();
    }

    public async Task<LeadRiskAssessment?> GetByIdAsync(int id)
    {
        return await _context.LeadRiskAssessments
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<LeadRiskAssessment>> GetOpenRisksAsync()
    {
        return await _context.LeadRiskAssessments
            .Where(x => x.IsActive && !x.IsResolved)
            .OrderByDescending(x => x.RiskScore)
            .ToListAsync();
    }

    public async Task<bool> ResolveAsync(int id)
    {
        var assessment = await _context.LeadRiskAssessments
            .FirstOrDefaultAsync(x => x.Id == id);

        if (assessment is null)
            return false;

        assessment.IsResolved = true;
        assessment.ResolvedAt = DateTime.UtcNow;
        assessment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateAsync(LeadRiskAssessment updated)
    {
        var assessment = await _context.LeadRiskAssessments
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (assessment is null)
            return false;

        assessment.UserId = updated.UserId;
        assessment.RiskCategory = updated.RiskCategory;
        assessment.RiskLevel = updated.RiskLevel;
        assessment.RiskScore = updated.RiskScore;
        assessment.Probability = updated.Probability;
        assessment.Impact = updated.Impact;
        assessment.Description = updated.Description;
        assessment.MitigationPlan = updated.MitigationPlan;
        assessment.IsResolved = updated.IsResolved;
        assessment.ResolvedAt = updated.ResolvedAt;
        assessment.Notes = updated.Notes;
        assessment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var assessment = await _context.LeadRiskAssessments
            .FirstOrDefaultAsync(x => x.Id == id);

        if (assessment is null)
            return false;

        assessment.IsActive = false;
        assessment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
