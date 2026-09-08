namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentResolution
{
    public int Id { get; set; }

    public int ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentId { get; set; }

    public string ResolvedBy { get; set; } = string.Empty;

    public DateTime ResolvedAt { get; set; } = DateTime.UtcNow;

    public string Resolution { get; set; } = string.Empty;

    public bool Reopened { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewComment? ReviewComment { get; set; }
}
