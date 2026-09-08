namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecution
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ValidationRuleResultId { get; set; }

    public Guid ExecutedByUserId { get; set; }

    public DateTime StartedAt { get; set; }

    public DateTime FinishedAt { get; set; }

    public bool Succeeded { get; set; }

    public string? ErrorMessage { get; set; }

    public string? ExecutionContext { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleResult? ValidationRuleResult { get; set; }
}
