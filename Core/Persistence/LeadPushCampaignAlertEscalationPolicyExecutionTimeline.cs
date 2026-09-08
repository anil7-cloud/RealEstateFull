namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertEscalationPolicyExecutionTimeline
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertEscalationPolicyExecutionId { get; set; }

    public string StageName { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public DateTime StartedAt { get; set; }

    public DateTime? FinishedAt { get; set; }

    public TimeSpan? Duration { get; set; }

    public string? Notes { get; set; }

    public LeadPushCampaignAlertEscalationPolicyExecution? PolicyExecution { get; set; }
}
