namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertRule
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public string RuleName { get; set; } = string.Empty;

    public string Metric { get; set; } = string.Empty;

    public string ComparisonOperator { get; set; } = string.Empty;

    public decimal ThresholdValue { get; set; }

    public bool IsEnabled { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaign? Campaign { get; set; }
}
