namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertEscalationPolicyExecutionStateRecoverySummary
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertEscalationPolicyExecutionStateRecoveryId { get; set; }

    public int TotalAttempts { get; set; }

    public int SuccessfulAttempts { get; set; }

    public int FailedAttempts { get; set; }

    public TimeSpan TotalRecoveryTime { get; set; }

    public string OverallStatus { get; set; } = string.Empty;

    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignAlertEscalationPolicyExecutionStateRecovery? Recovery { get; set; }
}
