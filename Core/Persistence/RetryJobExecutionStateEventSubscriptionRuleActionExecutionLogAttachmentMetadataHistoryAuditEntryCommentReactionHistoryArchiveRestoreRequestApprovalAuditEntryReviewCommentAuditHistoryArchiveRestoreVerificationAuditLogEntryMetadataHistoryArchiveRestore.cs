namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionLogAttachmentMetadataHistoryAuditEntryCommentReactionHistoryArchiveRestoreRequestApprovalAuditEntryReviewCommentAuditHistoryArchiveRestoreVerificationAuditLogEntryMetadataHistoryArchiveRestore
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid VerificationAuditLogEntryMetadataHistoryArchiveId { get; set; }

    public Guid RestoredByUserId { get; set; }

    public DateTime RestoredAt { get; set; } = DateTime.UtcNow;

    public bool IsRestored { get; set; }

    public string? RestoreReason { get; set; }

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionLogAttachmentMetadataHistoryAuditEntryCommentReactionHistoryArchiveRestoreRequestApprovalAuditEntryReviewCommentAuditHistoryArchive? VerificationAuditLogEntryMetadataHistoryArchive { get; set; }
}
