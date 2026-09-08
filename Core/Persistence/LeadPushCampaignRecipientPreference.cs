namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientPreference
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientId { get; set; }

    public bool AllowPushNotifications { get; set; } = true;

    public bool AllowMarketing { get; set; } = true;

    public bool AllowTransactional { get; set; } = true;

    public string PreferredLanguage { get; set; } = "tr";

    public string? TimeZone { get; set; }

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipient? Recipient { get; set; }
}
