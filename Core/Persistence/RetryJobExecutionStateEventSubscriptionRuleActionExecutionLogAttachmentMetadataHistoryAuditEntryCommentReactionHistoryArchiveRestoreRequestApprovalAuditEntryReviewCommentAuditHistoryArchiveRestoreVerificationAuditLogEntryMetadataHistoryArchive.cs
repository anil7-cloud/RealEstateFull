namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionLogAttachmentMetadataHistoryAuditEntryCommentReactionHistoryArchiveRestoreRequestApprovalAuditEntryReviewCommentAuditHistoryArchiveRestoreVerificationAuditLogEntryMetadataHistoryArchive
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid VerificationAuditLogEntryMetadataHistoryId { get; set; }

    public Guid ArchivedByUserId { get; set; }

    public DateTime ArchivedAt { get; set; } = DateTime.UtcNow;

    public bool IsArchived { get; set; }

    public string? ArchiveReason { get; set; }

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionLogAttachmentMetadataHistoryAuditEntryCommentReactionHistoryArchiveRestoreRequestApprovalAuditEntryReviewCommentAuditHistoryArchiveRestoreVerificationAuditLogEntryMetadataHistory? VerificationAuditLogEntryMetadataHistory { get; set; }
}
