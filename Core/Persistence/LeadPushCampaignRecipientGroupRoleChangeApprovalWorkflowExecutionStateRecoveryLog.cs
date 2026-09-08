namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryLog
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryId { get; set; }

    public string LogLevel { get; set; } = "Information";

    public string Message { get; set; } = string.Empty;

    public string? StackTrace { get; set; }

    public bool IsError { get; set; }

    public DateTime LoggedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecovery? Recovery { get; set; }
}
