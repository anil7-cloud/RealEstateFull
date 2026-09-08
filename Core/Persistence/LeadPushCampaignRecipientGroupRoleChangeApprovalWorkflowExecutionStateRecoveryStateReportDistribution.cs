namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDistribution
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportId { get; set; }

    public string Recipient { get; set; } = string.Empty;

    public string DeliveryChannel { get; set; } = string.Empty;

    public string DeliveryStatus { get; set; } = "Pending";

    public DateTime? DeliveredAt { get; set; }

    public string? FailureReason { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReport? Report { get; set; }
}
