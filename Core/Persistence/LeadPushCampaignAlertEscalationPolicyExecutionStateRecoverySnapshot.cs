namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertEscalationPolicyExecutionStateRecoverySnapshot
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertEscalationPolicyExecutionStateRecoveryId { get; set; }

    public string RecoveryState { get; set; } = string.Empty;

    public int CurrentAttempt { get; set; }

    public bool IsCompleted { get; set; }

    public string? SnapshotData { get; set; }

    public DateTime CapturedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignAlertEscalationPolicyExecutionStateRecovery? Recovery { get; set; }
}
