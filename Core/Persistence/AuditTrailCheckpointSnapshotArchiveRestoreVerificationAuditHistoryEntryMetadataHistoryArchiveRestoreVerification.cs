namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrailEntryMetadataHistoryAuditLogArchiveRestoreVerificationAuditTrailCheckpointSnapshotArchiveRestoreVerificationAuditHistoryEntryMetadataHistoryArchiveRestoreVerification
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid VerificationAuditHistoryEntryMetadataHistoryArchiveRestoreId { get; set; }

    public Guid VerifiedByUserId { get; set; }

    public DateTime VerifiedAt { get; set; } = DateTime.UtcNow;

    public bool IsVerified { get; set; }

    public string? VerificationNotes { get; set; }

    public string? VerificationChecksum { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrailEntryMetadataHistoryAuditLogArchiveRestoreVerificationAuditTrailCheckpointSnapshotArchiveRestoreVerificationAuditHistoryEntryMetadataHistoryArchiveRestore? VerificationAuditHistoryEntryMetadataHistoryArchiveRestore { get; set; }
}
