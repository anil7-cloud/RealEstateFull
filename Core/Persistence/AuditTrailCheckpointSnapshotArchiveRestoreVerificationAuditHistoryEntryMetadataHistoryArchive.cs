namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrailEntryMetadataHistoryAuditLogArchiveRestoreVerificationAuditTrailCheckpointSnapshotArchiveRestoreVerificationAuditHistoryEntryMetadataHistoryArchive
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid VerificationAuditHistoryEntryMetadataHistoryId { get; set; }

    public Guid ArchivedByUserId { get; set; }

    public DateTime ArchivedAt { get; set; } = DateTime.UtcNow;

    public string ArchiveReason { get; set; } = string.Empty;

    public bool IsEncrypted { get; set; }

    public string? Notes { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrailEntryMetadataHistoryAuditLogArchiveRestoreVerificationAuditTrailCheckpointSnapshotArchiveRestoreVerificationAuditHistoryEntryMetadataHistory? VerificationAuditHistoryEntryMetadataHistory { get; set; }
}
