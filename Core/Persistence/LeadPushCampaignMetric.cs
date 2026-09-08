namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignMetric
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public string MetricName { get; set; } = string.Empty;

    public decimal MetricValue { get; set; }

    public string Unit { get; set; } = string.Empty;

    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaign? Campaign { get; set; }
}
