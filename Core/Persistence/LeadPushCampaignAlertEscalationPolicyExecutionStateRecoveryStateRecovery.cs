namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertEscalationPolicyExecutionStateRecoveryStateRecovery
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertEscalationPolicyExecutionStateRecoveryStateId { get; set; }

    public string RecoveryMethod { get; set; } = string.Empty;

    public bool IsSuccessful { get; set; }

    public int AttemptNumber { get; set; }

    public string? FailureReason { get; set; }

    public string? RecoveryDetails { get; set; }

    public DateTime RecoveredAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignAlertEscalationPolicyExecutionStateRecoveryState? RecoveryState { get; set; }
}
