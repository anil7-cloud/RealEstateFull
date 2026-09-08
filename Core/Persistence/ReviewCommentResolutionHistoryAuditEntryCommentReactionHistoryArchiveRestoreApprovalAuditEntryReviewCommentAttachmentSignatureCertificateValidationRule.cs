namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationRule
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ValidationPolicyId { get; set; }

    public string RuleName { get; set; } = string.Empty;

    public string RuleCode { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int ExecutionOrder { get; set; }

    public bool IsRequired { get; set; }

    public bool IsEnabled { get; set; } = true;

    public ReviewCommentResolutionHistoryAuditEntryCommentReactionHistoryArchiveRestoreApprovalAuditEntryReviewCommentAttachmentSignatureCertificateValidationPolicy? ValidationPolicy { get; set; }
}
