namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrailEntryMetadataHistoryAuditLogArchiveRestoreVerificationAuditTrailCheckpointSnapshotArchiveRestoreVerificationAuditHistoryEntryMetadataHistoryArchiveRestoreVerificationAuditLog
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid VerificationAuditId { get; set; }

    public Guid LoggedByUserId { get; set; }

    public DateTime LoggedAt { get; set; } = DateTime.UtcNow;

    public string LogLevel { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string? Details { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrailEntryMetadataHistoryAuditLogArchiveRestoreVerificationAuditTrailCheckpointSnapshotArchiveRestoreVerificationAuditHistoryEntryMetadataHistoryArchiveRestoreVerificationAudit? VerificationAudit { get; set; }
}
