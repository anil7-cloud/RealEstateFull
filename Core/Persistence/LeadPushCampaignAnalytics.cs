namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAnalytics
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public int Impressions { get; set; }

    public int Deliveries { get; set; }

    public int Opens { get; set; }

    public int Clicks { get; set; }

    public int Conversions { get; set; }

    public decimal ClickThroughRate { get; set; }

    public decimal ConversionRate { get; set; }

    public decimal EngagementRate { get; set; }

    public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaign? Campaign { get; set; }
}
