namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignHealth
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public decimal HealthScore { get; set; }

    public string Status { get; set; } = "Healthy";

    public int WarningCount { get; set; }

    public int ErrorCount { get; set; }

    public decimal DeliverabilityScore { get; set; }

    public decimal EngagementScore { get; set; }

    public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaign? Campaign { get; set; }
}
