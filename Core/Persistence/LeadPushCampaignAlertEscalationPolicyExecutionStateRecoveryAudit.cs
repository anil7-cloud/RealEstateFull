namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertEscalationPolicyExecutionStateRecoveryAudit
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertEscalationPolicyExecutionStateRecoveryId { get; set; }

    public string EventType { get; set; } = string.Empty;

    public string Action { get; set; } = string.Empty;

    public string? PerformedBy { get; set; }

    public string? Details { get; set; }

    public bool IsSuccessful { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignAlertEscalationPolicyExecutionStateRecovery? Recovery { get; set; }
}
