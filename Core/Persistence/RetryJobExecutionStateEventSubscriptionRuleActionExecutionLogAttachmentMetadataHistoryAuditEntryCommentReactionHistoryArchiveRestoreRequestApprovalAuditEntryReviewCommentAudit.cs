namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionLogAttachmentMetadataHistoryAuditEntryCommentReactionHistoryArchiveRestoreRequestApprovalAuditEntryReviewCommentAudit
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ReviewCommentId { get; set; }

    public Guid AuditedByUserId { get; set; }

    public DateTime AuditedAt { get; set; } = DateTime.UtcNow;

    public bool IsApproved { get; set; }

    public string? AuditNotes { get; set; }

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionLogAttachmentMetadataHistoryAuditEntryCommentReactionHistoryArchiveRestoreRequestApprovalAuditEntryReviewComment? ReviewComment { get; set; }
}
