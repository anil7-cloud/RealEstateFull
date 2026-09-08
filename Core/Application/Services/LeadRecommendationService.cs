using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadRecommendationService
{
    private readonly AppDbContext _context;

    public LeadRecommendationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadRecommendation> CreateAsync(LeadRecommendation recommendation)
    {
        recommendation.CreatedAt = DateTime.UtcNow;
        recommendation.IsActive = true;

        _context.LeadRecommendations.Add(recommendation);

        await _context.SaveChangesAsync();

        return recommendation;
    }

    public async Task<List<LeadRecommendation>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadRecommendations
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderByDescending(x => x.RecommendationScore)
            .ThenByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<LeadRecommendation?> GetByIdAsync(int id)
    {
        return await _context.LeadRecommendations
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateAsync(LeadRecommendation updated)
    {
        var recommendation = await _context.LeadRecommendations
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (recommendation is null)
            return false;

        recommendation.RecommendationScore = updated.RecommendationScore;
        recommendation.RecommendationReason = updated.RecommendationReason;
        recommendation.IsSent = updated.IsSent;
        recommendation.SentAt = updated.SentAt;
        recommendation.IsViewed = updated.IsViewed;
        recommendation.ViewedAt = updated.ViewedAt;
        recommendation.IsInterested = updated.IsInterested;
        recommendation.Notes = updated.Notes;
        recommendation.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var recommendation = await _context.LeadRecommendations
            .FirstOrDefaultAsync(x => x.Id == id);

        if (recommendation is null)
            return false;

        recommendation.IsActive = false;
        recommendation.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
