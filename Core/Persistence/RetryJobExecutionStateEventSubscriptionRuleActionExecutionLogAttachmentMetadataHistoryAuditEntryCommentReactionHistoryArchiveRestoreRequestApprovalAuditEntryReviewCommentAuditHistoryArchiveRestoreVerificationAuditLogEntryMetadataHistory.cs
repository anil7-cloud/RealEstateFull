namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionLogAttachmentMetadataHistoryAuditEntryCommentReactionHistoryArchiveRestoreRequestApprovalAuditEntryReviewCommentAuditHistoryArchiveRestoreVerificationAuditLogEntryMetadataHistory
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid VerificationAuditLogEntryMetadataId { get; set; }

    public Guid ChangedByUserId { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    public string? ChangeSummary { get; set; }

    public string? PreviousValue { get; set; }

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionLogAttachmentMetadataHistoryAuditEntryCommentReactionHistoryArchiveRestoreRequestApprovalAuditEntryReviewCommentAuditHistoryArchiveRestoreVerificationAuditLogEntryMetadata? VerificationAuditLogEntryMetadata { get; set; }
}
