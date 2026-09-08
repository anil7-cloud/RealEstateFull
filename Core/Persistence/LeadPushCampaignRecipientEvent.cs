namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientEvent
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientId { get; set; }

    public string EventType { get; set; } = string.Empty;

    public string? EventValue { get; set; }

    public string? ProviderEventId { get; set; }

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public DateTime OccurredAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipient? Recipient { get; set; }
}
