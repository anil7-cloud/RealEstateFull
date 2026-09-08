namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentResolutionHistoryAuditEntryReviewCommentReaction
{
    public int Id { get; set; }

    public int ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentResolutionHistoryAuditEntryReviewCommentId { get; set; }

    public string ReactionType { get; set; } = string.Empty;

    public string ReactedBy { get; set; } = string.Empty;

    public DateTime ReactedAt { get; set; } = DateTime.UtcNow;

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentResolutionHistoryAuditEntryReviewComment? Comment { get; set; }
}
