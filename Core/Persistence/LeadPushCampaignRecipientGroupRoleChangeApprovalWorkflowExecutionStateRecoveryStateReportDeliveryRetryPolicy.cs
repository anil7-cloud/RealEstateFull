namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryPolicy
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDistributionId { get; set; }

    public int MaxRetryCount { get; set; }

    public int CurrentRetryCount { get; set; }

    public TimeSpan RetryInterval { get; set; }

    public bool UseExponentialBackoff { get; set; }

    public bool IsActive { get; set; }

    public DateTime? LastRetryAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDistribution? Distribution { get; set; }
}
