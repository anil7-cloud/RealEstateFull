namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientSession
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientId { get; set; }

    public string SessionId { get; set; } = string.Empty;

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public string? DeviceType { get; set; }

    public DateTime StartedAt { get; set; }

    public DateTime? EndedAt { get; set; }

    public LeadPushCampaignRecipient? Recipient { get; set; }
}
