namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrailEntry
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid AuditTrailId { get; set; }

    public int SequenceNumber { get; set; }

    public string EventName { get; set; } = string.Empty;

    public string? EventData { get; set; }

    public string? CorrelationId { get; set; }

    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;

    public Guid PerformedByUserId { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrail? AuditTrail { get; set; }
}
