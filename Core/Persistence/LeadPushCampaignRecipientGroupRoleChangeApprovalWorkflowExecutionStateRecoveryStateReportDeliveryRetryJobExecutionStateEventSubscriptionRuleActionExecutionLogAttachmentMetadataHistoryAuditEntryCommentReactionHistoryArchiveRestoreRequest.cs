namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionLogAttachmentMetadataHistoryAuditEntryCommentReactionHistoryArchiveRestoreRequest
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionLogAttachmentMetadataHistoryAuditEntryCommentReactionHistoryArchiveId { get; set; }

    public string RequestedBy { get; set; } = string.Empty;

    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

    public string Reason { get; set; } = string.Empty;

    public bool IsApproved { get; set; }

    public DateTime? ProcessedAt { get; set; }

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionLogAttachmentMetadataHistoryAuditEntryCommentReactionHistoryArchive? Archive { get; set; }
}
