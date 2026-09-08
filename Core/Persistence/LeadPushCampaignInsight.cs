namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignInsight
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public string InsightType { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Score { get; set; }

    public bool IsResolved { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaign? Campaign { get; set; }
}
