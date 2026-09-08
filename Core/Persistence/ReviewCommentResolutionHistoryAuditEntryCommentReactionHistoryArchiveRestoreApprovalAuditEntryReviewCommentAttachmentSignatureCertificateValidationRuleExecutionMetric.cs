namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionMetric
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid RuleExecutionId { get; set; }

    public string MetricName { get; set; } = string.Empty;

    public double MetricValue { get; set; }

    public string Unit { get; set; } = string.Empty;

    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

    public Guid RecordedByUserId { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecution? RuleExecution { get; set; }
}
