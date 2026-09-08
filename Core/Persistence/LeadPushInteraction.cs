namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushInteraction
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public int UserId { get; set; }

    public string InteractionType { get; set; } = string.Empty;

    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;

    public string Device { get; set; } = string.Empty;

    public string Platform { get; set; } = string.Empty;

    public string IpAddress { get; set; } = string.Empty;

    public LeadPushCampaign? Campaign { get; set; }
}
