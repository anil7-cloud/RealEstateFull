namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushNotificationTemplate
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Body { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public string IconUrl { get; set; } = string.Empty;

    public string ClickAction { get; set; } = string.Empty;

    public string DeepLink { get; set; } = string.Empty;

    public string Sound { get; set; } = "default";

    public string Priority { get; set; } = "High";

    public string Category { get; set; } = string.Empty;

    public string VariablesJson { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public bool IsDefault { get; set; }

    public int UsageCount { get; set; }

    public DateTime? LastUsedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
