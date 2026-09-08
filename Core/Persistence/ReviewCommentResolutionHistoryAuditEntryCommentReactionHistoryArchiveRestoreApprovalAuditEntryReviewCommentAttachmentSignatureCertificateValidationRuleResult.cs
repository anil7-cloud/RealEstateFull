namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleResult
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ValidationRuleId { get; set; }

    public Guid ValidationLogId { get; set; }

    public bool Passed { get; set; }

    public string? Details { get; set; }

    public TimeSpan ExecutionDuration { get; set; }

    public DateTime ExecutedAt { get; set; } = DateTime.UtcNow;

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRule? ValidationRule { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationLog? ValidationLog { get; set; }
}
