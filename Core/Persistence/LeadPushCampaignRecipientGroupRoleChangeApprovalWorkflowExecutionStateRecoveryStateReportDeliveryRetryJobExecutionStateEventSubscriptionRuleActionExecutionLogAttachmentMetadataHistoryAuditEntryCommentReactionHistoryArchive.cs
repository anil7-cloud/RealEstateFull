namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionLogAttachmentMetadataHistoryAuditEntryCommentReactionHistoryArchive
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionLogAttachmentMetadataHistoryAuditEntryCommentReactionHistoryId { get; set; }

    public DateTime ArchivedAt { get; set; } = DateTime.UtcNow;

    public string ArchivedBy { get; set; } = string.Empty;

    public string ArchiveReason { get; set; } = string.Empty;

    public bool IsCompressed { get; set; }

    public string? StorageLocation { get; set; }

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionLogAttachmentMetadataHistoryAuditEntryCommentReactionHistory? ReactionHistory { get; set; }
}
