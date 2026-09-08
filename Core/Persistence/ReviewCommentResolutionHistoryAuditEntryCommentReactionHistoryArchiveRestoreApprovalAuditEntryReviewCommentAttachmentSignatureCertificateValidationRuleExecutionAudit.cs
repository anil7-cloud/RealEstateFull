namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionAudit
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid RuleExecutionId { get; set; }

    public Guid PerformedByUserId { get; set; }

    public string Action { get; set; } = string.Empty;

    public bool WasSuccessful { get; set; }

    public string? Details { get; set; }

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecution? RuleExecution { get; set; }
}
