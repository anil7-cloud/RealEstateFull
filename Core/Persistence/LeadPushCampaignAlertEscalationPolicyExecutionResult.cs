namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertEscalationPolicyExecutionResult
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertEscalationPolicyExecutionId { get; set; }

    public bool IsSuccessful { get; set; }

    public int ExecutedLevels { get; set; }

    public int NotificationsSent { get; set; }

    public int ActionsExecuted { get; set; }

    public string? Summary { get; set; }

    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignAlertEscalationPolicyExecution? PolicyExecution { get; set; }
}
