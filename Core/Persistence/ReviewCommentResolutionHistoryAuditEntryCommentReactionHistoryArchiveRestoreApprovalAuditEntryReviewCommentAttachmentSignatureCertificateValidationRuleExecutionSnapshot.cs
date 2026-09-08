namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecutionSnapshot
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid RuleExecutionId { get; set; }

    public int SnapshotVersion { get; set; }

    public string StateJson { get; set; } = string.Empty;

    public string? ConfigurationHash { get; set; }

    public DateTime CapturedAt { get; set; } = DateTime.UtcNow;

    public Guid CapturedByUserId { get; set; }

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRuleExecution? RuleExecution { get; set; }
}
