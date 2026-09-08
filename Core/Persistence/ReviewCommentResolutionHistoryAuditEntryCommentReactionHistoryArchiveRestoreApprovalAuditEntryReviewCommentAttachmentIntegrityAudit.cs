namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentIntegrityAudit
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid AttachmentIntegrityCheckId { get; set; }

    public Guid AttachmentId { get; set; }

    public Guid VerifiedByUserId { get; set; }

    public bool VerificationSucceeded { get; set; }

    public string? FailureReason { get; set; }

    public string? ExpectedHash { get; set; }

    public string? ActualHash { get; set; }

    public DateTime VerifiedAt { get; set; } = DateTime.UtcNow;

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentIntegrityCheck? IntegrityCheck { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachment? Attachment { get; set; }
}
