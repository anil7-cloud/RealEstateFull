namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentResolutionHistoryAuditEntryReviewCommentReactionHistoryArchiveRestoreApproval
{
    public int Id { get; set; }

    public int ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentResolutionHistoryAuditEntryReviewCommentReactionHistoryArchiveRestoreRequestId { get; set; }

    public string ApprovedBy { get; set; } = string.Empty;

    public DateTime ApprovedAt { get; set; } = DateTime.UtcNow;

    public bool IsApproved { get; set; }

    public string? Notes { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentResolutionHistoryAuditEntryReviewCommentReactionHistoryArchiveRestoreRequest? RestoreRequest { get; set; }
}
