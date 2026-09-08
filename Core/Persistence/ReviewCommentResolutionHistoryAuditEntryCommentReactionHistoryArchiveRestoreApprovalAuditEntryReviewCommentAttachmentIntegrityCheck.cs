namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentIntegrityCheck
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid AttachmentId { get; set; }

    public string HashAlgorithm { get; set; } = string.Empty;

    public string HashValue { get; set; } = string.Empty;

    public bool IsValid { get; set; }

    public DateTime VerifiedAt { get; set; } = DateTime.UtcNow;

    public Guid VerifiedByUserId { get; set; }

    public string? Notes { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachment? Attachment { get; set; }
}
