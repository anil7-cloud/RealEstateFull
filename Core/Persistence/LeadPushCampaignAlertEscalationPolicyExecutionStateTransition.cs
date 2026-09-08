namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertEscalationPolicyExecutionStateTransition
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertEscalationPolicyExecutionStateId { get; set; }

    public string FromState { get; set; } = string.Empty;

    public string ToState { get; set; } = string.Empty;

    public string Trigger { get; set; } = string.Empty;

    public bool IsSuccessful { get; set; }

    public string? Notes { get; set; }

    public DateTime TransitionedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignAlertEscalationPolicyExecutionState? ExecutionState { get; set; }
}
