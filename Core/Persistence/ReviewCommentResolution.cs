namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class ReviewCommentResolution
{
    public int Id { get; set; }

    public int ReviewCommentId { get; set; }

    public string ResolvedBy { get; set; } = string.Empty;

    public DateTime ResolvedAt { get; set; } = DateTime.UtcNow;

    public string Resolution { get; set; } = string.Empty;

    public bool Reopened { get; set; }

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionLogAttachmentMetadataHistoryAuditEntryCommentReactionHistoryArchiveRestoreRequestApprovalAuditEntryReviewComment? ReviewComment { get; set; }
}
