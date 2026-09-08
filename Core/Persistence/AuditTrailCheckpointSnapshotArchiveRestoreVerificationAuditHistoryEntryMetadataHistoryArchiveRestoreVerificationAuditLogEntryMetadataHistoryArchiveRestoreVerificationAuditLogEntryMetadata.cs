namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrailEntryMetadataHistoryAuditLogArchiveRestoreVerificationAuditTrailCheckpointSnapshotArchiveRestoreVerificationAuditHistoryEntryMetadataHistoryArchiveRestoreVerificationAuditLogEntryMetadataHistoryArchiveRestoreVerificationAuditLogEntryMetadata
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid VerificationAuditLogEntryId { get; set; }

    public Guid CreatedByUserId { get; set; }

    public string Key { get; set; } = string.Empty;

    public string? Value { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsSystemGenerated { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrailEntryMetadataHistoryAuditLogArchiveRestoreVerificationAuditTrailCheckpointSnapshotArchiveRestoreVerificationAuditHistoryEntryMetadataHistoryArchiveRestoreVerificationAuditLogEntryMetadataHistoryArchiveRestoreVerificationAuditLogEntry? VerificationAuditLogEntry { get; set; }
}
