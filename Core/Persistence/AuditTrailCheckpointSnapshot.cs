namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrailEntryMetadataHistoryAuditLogArchiveRestoreVerificationAuditTrailCheckpointSnapshot
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid AuditTrailCheckpointId { get; set; }

    public Guid CreatedByUserId { get; set; }

    public DateTime SnapshotCreatedAt { get; set; } = DateTime.UtcNow;

    public string SnapshotVersion { get; set; } = string.Empty;

    public string? SnapshotData { get; set; }

    public bool IsCompressed { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrailEntryMetadataHistoryAuditLogArchiveRestoreVerificationAuditTrailCheckpoint? AuditTrailCheckpoint { get; set; }
}
