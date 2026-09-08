namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApproval
{
    public int Id { get; set; }

    public int ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreRequestId { get; set; }

    public string ApprovedBy { get; set; } = string.Empty;

    public DateTime ApprovedAt { get; set; } = DateTime.UtcNow;

    public bool IsApproved { get; set; }

    public string? Notes { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreRequest? RestoreRequest { get; set; }
}
