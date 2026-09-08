namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadNotificationQueue
{
    public int Id { get; set; }

    public int? LeadNotificationTemplateId { get; set; }

    public int? UserId { get; set; }

    public int? LeadId { get; set; }

    public string Recipient { get; set; } = string.Empty;

    public string Channel { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string VariablesJson { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public int RetryCount { get; set; }

    public int MaxRetryCount { get; set; } = 3;

    public DateTime? NextRetryAt { get; set; }

    public DateTime? SentAt { get; set; }

    public string ErrorMessage { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
