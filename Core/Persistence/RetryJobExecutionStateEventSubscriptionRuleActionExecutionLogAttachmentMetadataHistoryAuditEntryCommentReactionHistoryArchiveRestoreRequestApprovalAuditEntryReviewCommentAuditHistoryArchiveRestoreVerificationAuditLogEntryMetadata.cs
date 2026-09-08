namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionLogAttachmentMetadataHistoryAuditEntryCommentReactionHistoryArchiveRestoreRequestApprovalAuditEntryReviewCommentAuditHistoryArchiveRestoreVerificationAuditLogEntryMetadata
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid VerificationAuditLogEntryId { get; set; }

    public Guid RecordedByUserId { get; set; }

    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

    public string? MetadataKey { get; set; }

    public string? MetadataValue { get; set; }

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionLogAttachmentMetadataHistoryAuditEntryCommentReactionHistoryArchiveRestoreRequestApprovalAuditEntryReviewCommentAuditHistoryArchiveRestoreVerificationAuditLogEntry? VerificationAuditLogEntry { get; set; }
}
