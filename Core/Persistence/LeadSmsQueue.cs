namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadSmsQueue
{
    public int Id { get; set; }

    public int? LeadSmsTemplateId { get; set; }

    public int? UserId { get; set; }

    public int? LeadId { get; set; }

    public string PhoneNumber { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string SenderName { get; set; } = string.Empty;

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
