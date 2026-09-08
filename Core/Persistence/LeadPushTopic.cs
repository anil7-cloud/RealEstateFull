namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushTopic
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public bool IsDefault { get; set; }

    public int SubscriberCount { get; set; }

    public int NotificationCount { get; set; }

    public DateTime? LastNotificationAt { get; set; }

    public string Icon { get; set; } = string.Empty;

    public string Color { get; set; } = "#2196F3";

    public string Category { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
