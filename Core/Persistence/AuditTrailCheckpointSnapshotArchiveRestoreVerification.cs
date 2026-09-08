namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrailEntryMetadataHistoryAuditLogArchiveRestoreVerificationAuditTrailCheckpointSnapshotArchiveRestoreVerification
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid AuditTrailCheckpointSnapshotArchiveRestoreId { get; set; }

    public Guid VerifiedByUserId { get; set; }

    public DateTime VerifiedAt { get; set; } = DateTime.UtcNow;

    public bool IsVerified { get; set; }

    public string? VerificationDetails { get; set; }

    public string? Checksum { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrailEntryMetadataHistoryAuditLogArchiveRestoreVerificationAuditTrailCheckpointSnapshotArchiveRestore? AuditTrailCheckpointSnapshotArchiveRestore { get; set; }
}
