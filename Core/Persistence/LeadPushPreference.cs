namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushPreference
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public bool PushEnabled { get; set; } = true;

    public bool EmailEnabled { get; set; } = true;

    public bool SmsEnabled { get; set; }

    public bool MarketingEnabled { get; set; } = true;

    public bool SystemNotificationsEnabled { get; set; } = true;

    public bool NewPropertyAlertsEnabled { get; set; } = true;

    public bool PriceDropAlertsEnabled { get; set; } = true;

    public bool FavoritePropertyUpdatesEnabled { get; set; } = true;

    public string PreferredLanguage { get; set; } = "tr";

    public string TimeZone { get; set; } = "Europe/Istanbul";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
