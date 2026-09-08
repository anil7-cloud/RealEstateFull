namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrailEntryMetadataHistoryAuditLogArchiveRestoreVerificationAuditTrailCheckpoint
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid AuditTrailId { get; set; }

    public Guid CreatedByUserId { get; set; }

    public string CheckpointName { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string? Notes { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrailEntryMetadataHistoryAuditLogArchiveRestoreVerificationAuditTrail? AuditTrail { get; set; }
}
