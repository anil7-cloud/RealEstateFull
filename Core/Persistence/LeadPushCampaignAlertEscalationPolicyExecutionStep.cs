namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertEscalationPolicyExecutionStep
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertEscalationPolicyExecutionId { get; set; }

    public int PolicyLevelId { get; set; }

    public string StepName { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public bool IsSuccessful { get; set; }

    public string? ResultMessage { get; set; }

    public DateTime ExecutedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignAlertEscalationPolicyExecution? PolicyExecution { get; set; }

    public LeadPushCampaignAlertEscalationPolicyLevel? PolicyLevel { get; set; }
}
