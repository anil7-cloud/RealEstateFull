namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertEscalationPolicyExecutionStateRecoveryStateHistory
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertEscalationPolicyExecutionStateRecoveryStateId { get; set; }

    public string PreviousState { get; set; } = string.Empty;

    public string CurrentState { get; set; } = string.Empty;

    public string? ChangedBy { get; set; }

    public string? Notes { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignAlertEscalationPolicyExecutionStateRecoveryState? RecoveryState { get; set; }
}
