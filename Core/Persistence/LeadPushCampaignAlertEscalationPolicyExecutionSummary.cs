namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertEscalationPolicyExecutionSummary
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertEscalationPolicyExecutionId { get; set; }

    public int TotalLevelsProcessed { get; set; }

    public int SuccessfulLevels { get; set; }

    public int FailedLevels { get; set; }

    public int TotalNotificationsSent { get; set; }

    public int TotalActionsExecuted { get; set; }

    public TimeSpan TotalExecutionTime { get; set; }

    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignAlertEscalationPolicyExecution? PolicyExecution { get; set; }
}
