namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientActivity
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientId { get; set; }

    public string ActivityType { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? IpAddress { get; set; }

    public string? DeviceInfo { get; set; }

    public string? Location { get; set; }

    public DateTime ActivityAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipient? Recipient { get; set; }
}
