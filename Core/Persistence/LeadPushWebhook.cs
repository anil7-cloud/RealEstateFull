namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushWebhook
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    public string SecretKey { get; set; } = string.Empty;

    public string EventType { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public int RetryCount { get; set; }

    public DateTime? LastTriggeredAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
