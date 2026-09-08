namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrailEntryMetadataHistoryAuditLogArchiveRestoreVerificationAuditTrailCheckpointSnapshotArchiveRestoreVerificationAuditHistoryEntryMetadataHistoryArchiveRestoreVerificationAuditLogEntry
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid VerificationAuditLogId { get; set; }

    public Guid CreatedByUserId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string EntryType { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? Metadata { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrailEntryMetadataHistoryAuditLogArchiveRestoreVerificationAuditTrailCheckpointSnapshotArchiveRestoreVerificationAuditHistoryEntryMetadataHistoryArchiveRestoreVerificationAuditLog? VerificationAuditLog { get; set; }
}
