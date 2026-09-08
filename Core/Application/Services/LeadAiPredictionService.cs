using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadAiPredictionService
{
    private readonly AppDbContext _context;

    public LeadAiPredictionService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadAiPrediction> CreateAsync(
        int leadId,
        string predictionType,
        decimal confidenceScore,
        string recommendation)
    {
        var prediction = new LeadAiPrediction
        {
            LeadId = leadId,
            PredictionType = predictionType,
            ConfidenceScore = confidenceScore,
            Recommendation = recommendation,
            NextBestAction = string.Empty,
            AiModel = "Default AI Model",
            Explanation = string.Empty,
            PredictedConversionRate = 0,
            PredictedRevenue = 0,
            PredictedCloseDate = DateTime.UtcNow.AddDays(30),
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadAiPredictions.Add(prediction);

        await _context.SaveChangesAsync();

        return prediction;
    }


    public async Task<List<LeadAiPrediction>> GetByLeadAsync(
        int leadId)
    {
        return await _context.LeadAiPredictions
            .Where(x => x.LeadId == leadId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<LeadAiPrediction?> GetLatestAsync(
        int leadId)
    {
        return await _context.LeadAiPredictions
            .Where(x => x.LeadId == leadId)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync();
    }
}
