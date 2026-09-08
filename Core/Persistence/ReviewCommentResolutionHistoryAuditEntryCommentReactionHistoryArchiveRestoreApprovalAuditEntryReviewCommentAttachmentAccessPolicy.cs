namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentAccessPolicy
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid AttachmentId { get; set; }

    public Guid RoleId { get; set; }

    public bool CanView { get; set; }

    public bool CanDownload { get; set; }

    public bool CanReplace { get; set; }

    public bool CanDelete { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid CreatedByUserId { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachment? Attachment { get; set; }
}
