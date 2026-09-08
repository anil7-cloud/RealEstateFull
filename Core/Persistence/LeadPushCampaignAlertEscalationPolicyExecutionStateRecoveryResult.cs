namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertEscalationPolicyExecutionStateRecoveryResult
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertEscalationPolicyExecutionStateRecoveryId { get; set; }

    public bool IsSuccessful { get; set; }

    public int AttemptCount { get; set; }

    public TimeSpan RecoveryDuration { get; set; }

    public string? FailureReason { get; set; }

    public string? Summary { get; set; }

    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignAlertEscalationPolicyExecutionStateRecovery? Recovery { get; set; }
}
