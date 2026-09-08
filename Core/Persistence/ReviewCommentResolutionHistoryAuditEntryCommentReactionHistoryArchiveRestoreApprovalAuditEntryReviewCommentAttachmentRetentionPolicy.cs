namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentRetentionPolicy
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid AttachmentId { get; set; }

    public int RetentionDays { get; set; }

    public bool AutoDeleteEnabled { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid CreatedByUserId { get; set; }

    public string? Notes { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachment? Attachment { get; set; }
}
