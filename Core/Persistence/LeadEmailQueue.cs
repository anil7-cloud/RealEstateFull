namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadEmailQueue
{
    public int Id { get; set; }

    public int? LeadEmailTemplateId { get; set; }

    public int? UserId { get; set; }

    public int? LeadId { get; set; }

    public string ToEmail { get; set; } = string.Empty;

    public string CcEmail { get; set; } = string.Empty;

    public string BccEmail { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty;

    public string HtmlBody { get; set; } = string.Empty;

    public string PlainTextBody { get; set; } = string.Empty;

    public string AttachmentsJson { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public int RetryCount { get; set; }

    public int MaxRetryCount { get; set; } = 3;

    public DateTime? NextRetryAt { get; set; }

    public DateTime? SentAt { get; set; }

    public bool IsCompleted { get; set; }

    public string ErrorMessage { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
