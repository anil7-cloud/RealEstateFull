namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestore
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid SnapshotArchiveId { get; set; }

    public Guid RestoredByUserId { get; set; }

    public DateTime RestoredAt { get; set; } = DateTime.UtcNow;

    public string RestoreLocation { get; set; } = string.Empty;

    public bool IsSuccessful { get; set; }

    public string? FailureReason { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchive? SnapshotArchive { get; set; }
}
