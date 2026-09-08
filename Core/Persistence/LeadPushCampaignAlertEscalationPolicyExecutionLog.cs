namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertEscalationPolicyExecutionLog
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertEscalationPolicyExecutionId { get; set; }

    public string LogLevel { get; set; } = "Information";

    public string Message { get; set; } = string.Empty;

    public string? Details { get; set; }

    public bool IsError { get; set; }

    public DateTime LoggedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignAlertEscalationPolicyExecution? PolicyExecution { get; set; }
}
