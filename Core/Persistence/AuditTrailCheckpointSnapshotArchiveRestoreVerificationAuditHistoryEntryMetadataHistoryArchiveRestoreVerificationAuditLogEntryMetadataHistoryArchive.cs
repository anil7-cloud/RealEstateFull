namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrailEntryMetadataHistoryAuditLogArchiveRestoreVerificationAuditTrailCheckpointSnapshotArchiveRestoreVerificationAuditHistoryEntryMetadataHistoryArchiveRestoreVerificationAuditLogEntryMetadataHistoryArchive
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid VerificationAuditLogEntryMetadataHistoryId { get; set; }

    public Guid ArchivedByUserId { get; set; }

    public DateTime ArchivedAt { get; set; } = DateTime.UtcNow;

    public string ArchiveReason { get; set; } = string.Empty;

    public bool IsEncrypted { get; set; }

    public string? Notes { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrailEntryMetadataHistoryAuditLogArchiveRestoreVerificationAuditTrailCheckpointSnapshotArchiveRestoreVerificationAuditHistoryEntryMetadataHistoryArchiveRestoreVerificationAuditLogEntryMetadataHistory? VerificationAuditLogEntryMetadataHistory { get; set; }
}
