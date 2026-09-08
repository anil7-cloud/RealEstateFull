namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertEscalationAction
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertEscalationId { get; set; }

    public string ActionName { get; set; } = string.Empty;

    public string ActionType { get; set; } = string.Empty;

    public bool IsSuccessful { get; set; }

    public string? ResultMessage { get; set; }

    public string? ExecutedBy { get; set; }

    public DateTime ExecutedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignAlertEscalation? Escalation { get; set; }
}
