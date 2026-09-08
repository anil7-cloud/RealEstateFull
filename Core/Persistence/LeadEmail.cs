namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadEmail
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int? PropertyId { get; set; }

    public int? UserId { get; set; }

    public string RecipientEmail { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty;

    public string Body { get; set; } = string.Empty;

    // Draft, Queued, Sent, Failed
    public string Status { get; set; } = "Draft";

    public DateTime? SentAt { get; set; }

    public bool IsHtml { get; set; }

    public bool HasAttachment { get; set; }

    public string AttachmentPath { get; set; } = string.Empty;

    public string ErrorMessage { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
