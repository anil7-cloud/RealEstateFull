namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignTrigger
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public string TriggerName { get; set; } = string.Empty;

    public string TriggerType { get; set; } = string.Empty;

    public string? TriggerValue { get; set; }

    public bool IsEnabled { get; set; } = true;

    public DateTime? LastExecutedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaign? Campaign { get; set; }
}
