namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrailEntryMetadataHistoryAuditLogArchiveRestoreVerificationAuditTrailCheckpointSnapshotArchiveRestoreVerificationAuditHistoryEntryMetadata
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid VerificationAuditHistoryEntryId { get; set; }

    public Guid CreatedByUserId { get; set; }

    public string Key { get; set; } = string.Empty;

    public string? Value { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsSystemGenerated { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrailEntryMetadataHistoryAuditLogArchiveRestoreVerificationAuditTrailCheckpointSnapshotArchiveRestoreVerificationAuditHistoryEntry? VerificationAuditHistoryEntry { get; set; }
}
