namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrailEntryMetadataHistoryAuditLogArchiveRestoreVerificationAuditTrailCheckpointSnapshotArchiveRestoreVerificationAudit
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ArchiveRestoreVerificationId { get; set; }

    public Guid AuditedByUserId { get; set; }

    public DateTime AuditedAt { get; set; } = DateTime.UtcNow;

    public bool Passed { get; set; }

    public string? Findings { get; set; }

    public string? Recommendations { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrailEntryMetadataHistoryAuditLogArchiveRestoreVerificationAuditTrailCheckpointSnapshotArchiveRestoreVerification? ArchiveRestoreVerification { get; set; }
}
