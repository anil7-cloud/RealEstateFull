namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushNotificationQueue
{
    public int Id { get; set; }

    public int? LeadPushNotificationTemplateId { get; set; }

    public int? UserId { get; set; }

    public int? LeadId { get; set; }

    public string DeviceToken { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Body { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public string DataJson { get; set; } = string.Empty;

    public string Priority { get; set; } = "High";

    public string Status { get; set; } = "Pending";

    public int RetryCount { get; set; }

    public int MaxRetryCount { get; set; } = 3;

    public DateTime? NextRetryAt { get; set; }

    public DateTime? SentAt { get; set; }

    public bool IsCompleted { get; set; }

    public string ProviderMessageId { get; set; } = string.Empty;

    public string ProviderResponse { get; set; } = string.Empty;

    public string ErrorMessage { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
