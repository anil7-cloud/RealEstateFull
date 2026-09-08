namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentAccessAudit
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid AttachmentId { get; set; }

    public Guid UserId { get; set; }

    public string Action { get; set; } = string.Empty;

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public bool IsSuccessful { get; set; }

    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachment? Attachment { get; set; }
}
