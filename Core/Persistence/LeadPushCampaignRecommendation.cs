namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecommendation
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public string RecommendationType { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Priority { get; set; }

    public bool IsApplied { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? AppliedAt { get; set; }

    public LeadPushCampaign? Campaign { get; set; }
}
