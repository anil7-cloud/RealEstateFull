namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignPrediction
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public string PredictionType { get; set; } = string.Empty;

    public decimal PredictedOpenRate { get; set; }

    public decimal PredictedClickRate { get; set; }

    public decimal PredictedConversionRate { get; set; }

    public decimal ConfidenceScore { get; set; }

    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaign? Campaign { get; set; }
}
