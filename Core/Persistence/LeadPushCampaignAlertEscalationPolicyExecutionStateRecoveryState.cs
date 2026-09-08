namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertEscalationPolicyExecutionStateRecoveryState
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertEscalationPolicyExecutionStateRecoveryId { get; set; }

    public string PreviousState { get; set; } = string.Empty;

    public string CurrentState { get; set; } = string.Empty;

    public string? TransitionReason { get; set; }

    public bool IsFinalState { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignAlertEscalationPolicyExecutionStateRecovery? Recovery { get; set; }
}
