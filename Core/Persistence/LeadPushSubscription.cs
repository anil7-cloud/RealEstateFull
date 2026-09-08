namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushSubscription
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int LeadPushTopicId { get; set; }

    public int? LeadPushDeviceId { get; set; }

    public string DeviceToken { get; set; } = string.Empty;

    public bool IsSubscribed { get; set; } = true;

    public DateTime? SubscribedAt { get; set; }

    public DateTime? UnsubscribedAt { get; set; }

    public string Language { get; set; } = "tr";

    public string Platform { get; set; } = string.Empty;

    public bool NotificationsEnabled { get; set; } = true;

    public DateTime? LastNotificationAt { get; set; }

    public int NotificationCount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
