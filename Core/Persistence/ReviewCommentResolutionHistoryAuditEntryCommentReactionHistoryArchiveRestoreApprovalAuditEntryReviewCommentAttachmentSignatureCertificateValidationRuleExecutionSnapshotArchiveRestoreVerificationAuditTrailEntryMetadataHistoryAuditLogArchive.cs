namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrailEntryMetadataHistoryAuditLogArchive
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid AuditLogId { get; set; }

    public string ArchiveLocation { get; set; } = string.Empty;

    public string StorageProvider { get; set; } = string.Empty;

    public long ArchiveSize { get; set; }

    public string Checksum { get; set; } = string.Empty;

    public Guid ArchivedByUserId { get; set; }

    public DateTime ArchivedAt { get; set; } = DateTime.UtcNow;

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrailEntryMetadataHistoryAuditLog? AuditLog { get; set; }
}
