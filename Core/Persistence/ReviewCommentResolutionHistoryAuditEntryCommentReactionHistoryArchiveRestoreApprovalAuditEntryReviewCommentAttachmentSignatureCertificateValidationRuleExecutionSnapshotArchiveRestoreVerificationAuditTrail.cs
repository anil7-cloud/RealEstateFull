namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAuditTrail
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid VerificationAuditId { get; set; }

    public Guid PerformedByUserId { get; set; }

    public string EventType { get; set; } = string.Empty;

    public string? PreviousState { get; set; }

    public string? CurrentState { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshotArchiveRestoreVerificationAudit? VerificationAudit { get; set; }
}
