namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignComparison
{
    public int Id { get; set; }

    public int SourceCampaignId { get; set; }

    public int TargetCampaignId { get; set; }

    public decimal OpenRateDifference { get; set; }

    public decimal ClickRateDifference { get; set; }

    public decimal ConversionRateDifference { get; set; }

    public string WinnerMetric { get; set; } = string.Empty;

    public DateTime ComparedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaign? SourceCampaign { get; set; }

    public LeadPushCampaign? TargetCampaign { get; set; }
}
