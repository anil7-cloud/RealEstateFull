namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionLogAttachmentMetadataHistoryAuditEntryCommentReaction
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionLogAttachmentMetadataHistoryAuditEntryCommentId { get; set; }

    public string ReactionType { get; set; } = string.Empty;

    public string ReactedBy { get; set; } = string.Empty;

    public DateTime ReactedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionLogAttachmentMetadataHistoryAuditEntryComment? Comment { get; set; }
}
