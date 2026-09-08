namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientDevice
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientId { get; set; }

    public string DeviceId { get; set; } = string.Empty;

    public string DeviceType { get; set; } = string.Empty;

    public string? OperatingSystem { get; set; }

    public string? Browser { get; set; }

    public string? AppVersion { get; set; }

    public bool IsCurrentDevice { get; set; }

    public DateTime LastSeenAt { get; set; }

    public LeadPushCampaignRecipient? Recipient { get; set; }
}
