namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentResolutionHistoryAuditEntryReviewCommentReactionHistoryArchiveRestoreRequest
{
    public int Id { get; set; }

    public int ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentResolutionHistoryAuditEntryReviewCommentReactionHistoryArchiveId { get; set; }

    public string RequestedBy { get; set; } = string.Empty;

    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

    public string Reason { get; set; } = string.Empty;

    public bool IsApproved { get; set; }

    public DateTime? ProcessedAt { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentResolutionHistoryAuditEntryReviewCommentReactionHistoryArchive? Archive { get; set; }
}
