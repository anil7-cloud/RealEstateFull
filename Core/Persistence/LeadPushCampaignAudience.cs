namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAudience
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public int LeadPushAudienceId { get; set; }

    public bool IsPrimaryAudience { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaign? Campaign { get; set; }

    public LeadPushAudience? Audience { get; set; }
}
