namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrailEntryMetadataHistoryAuditLog
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid MetadataHistoryAuditId { get; set; }

    public Guid LoggedByUserId { get; set; }

    public DateTime LoggedAt { get; set; } = DateTime.UtcNow;

    public string LogLevel { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string? Exception { get; set; }

    public string? CorrelationId { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrailEntryMetadataHistoryAudit? MetadataHistoryAudit { get; set; }
}
