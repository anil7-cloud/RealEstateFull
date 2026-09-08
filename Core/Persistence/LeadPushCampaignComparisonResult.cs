namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignComparisonResult
{
    public int Id { get; set; }

    public int LeadPushCampaignComparisonId { get; set; }

    public string MetricName { get; set; } = string.Empty;

    public decimal SourceValue { get; set; }

    public decimal TargetValue { get; set; }

    public decimal Difference { get; set; }

    public decimal DifferencePercentage { get; set; }

    public string Winner { get; set; } = string.Empty;

    public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignComparison? Comparison { get; set; }
}
