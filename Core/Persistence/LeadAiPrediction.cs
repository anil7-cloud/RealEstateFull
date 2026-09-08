namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadAiPrediction
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int? UserId { get; set; }

    public string PredictionType { get; set; } = string.Empty;

    public decimal ConfidenceScore { get; set; }

    public decimal PredictedConversionRate { get; set; }

    public decimal PredictedRevenue { get; set; }

    public DateTime PredictedCloseDate { get; set; }

    public string Recommendation { get; set; } = string.Empty;

    public string NextBestAction { get; set; } = string.Empty;

    public string AiModel { get; set; } = string.Empty;

    public string Explanation { get; set; } = string.Empty;

    public bool IsApplied { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
