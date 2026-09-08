namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionLogAttachmentMetadataHistoryAuditEntryCommentReactionHistoryArchiveRestoreRequestApprovalAuditEntryReviewCommentAuditHistoryArchiveRestoreVerificationAuditLog
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid VerificationAuditId { get; set; }

    public Guid LoggedByUserId { get; set; }

    public DateTime LoggedAt { get; set; } = DateTime.UtcNow;

    public string? LogMessage { get; set; }

    public string? Severity { get; set; }

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionLogAttachmentMetadataHistoryAuditEntryCommentReactionHistoryArchiveRestoreRequestApprovalAuditEntryReviewCommentAuditHistoryArchiveRestoreVerificationAudit? VerificationAudit { get; set; }
}
