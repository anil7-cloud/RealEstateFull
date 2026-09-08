namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientTimeline
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientId { get; set; }

    public string TimelineEvent { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string? Details { get; set; }

    public string? Source { get; set; }

    public DateTime EventAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipient? Recipient { get; set; }
}
