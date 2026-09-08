using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadScoreService
{
    private readonly AppDbContext _context;

    public LeadScoreService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadScore> CreateAsync(LeadScore score)
    {
        score.CreatedAt = DateTime.UtcNow;
        score.CalculatedAt = DateTime.UtcNow;
        score.IsActive = true;

        _context.LeadScores.Add(score);

        await _context.SaveChangesAsync();

        return score;
    }

    public async Task<LeadScore?> GetByLeadAsync(int leadId)
    {
        return await _context.LeadScores
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderByDescending(x => x.CalculatedAt)
            .FirstOrDefaultAsync();
    }

    public async Task<LeadScore?> GetByIdAsync(int id)
    {
        return await _context.LeadScores
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateAsync(LeadScore updated)
    {
        var score = await _context.LeadScores
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (score is null)
            return false;

        score.Score = updated.Score;
        score.BudgetScore = updated.BudgetScore;
        score.InterestScore = updated.InterestScore;
        score.CommunicationScore = updated.CommunicationScore;
        score.EngagementScore = updated.EngagementScore;
        score.UrgencyScore = updated.UrgencyScore;
        score.Category = updated.Category;
        score.CalculatedAt = DateTime.UtcNow;
        score.Notes = updated.Notes;
        score.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var score = await _context.LeadScores
            .FirstOrDefaultAsync(x => x.Id == id);

        if (score is null)
            return false;

        score.IsActive = false;
        score.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
