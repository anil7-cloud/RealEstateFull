namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertEscalationPolicyExecutionStateSnapshot
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertEscalationPolicyExecutionStateId { get; set; }

    public string StateName { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string? SnapshotData { get; set; }

    public int RetryCount { get; set; }

    public bool IsRecoverable { get; set; }

    public DateTime CapturedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignAlertEscalationPolicyExecutionState? ExecutionState { get; set; }
}
