namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoverySummary
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryId { get; set; }

    public int TotalAttempts { get; set; }

    public int SuccessfulAttempts { get; set; }

    public int FailedAttempts { get; set; }

    public TimeSpan TotalRecoveryDuration { get; set; }

    public string OverallStatus { get; set; } = string.Empty;

    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecovery? Recovery { get; set; }
}
