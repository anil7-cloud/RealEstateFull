namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignTagMap
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public int LeadPushCampaignTagId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaign? Campaign { get; set; }

    public LeadPushCampaignTag? Tag { get; set; }
}
