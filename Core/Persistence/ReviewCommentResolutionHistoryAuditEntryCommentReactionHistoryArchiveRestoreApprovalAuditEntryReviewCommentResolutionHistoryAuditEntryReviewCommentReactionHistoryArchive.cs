namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentResolutionHistoryAuditEntryReviewCommentReactionHistoryArchive
{
    public int Id { get; set; }

    public int ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentResolutionHistoryAuditEntryReviewCommentReactionHistoryId { get; set; }

    public DateTime ArchivedAt { get; set; } = DateTime.UtcNow;

    public string ArchivedBy { get; set; } = string.Empty;

    public string ArchiveReason { get; set; } = string.Empty;

    public bool IsCompressed { get; set; }

    public string? StorageLocation { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentResolutionHistoryAuditEntryReviewCommentReactionHistory? ReactionHistory { get; set; }
}
