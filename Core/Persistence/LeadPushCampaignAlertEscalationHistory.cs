namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertEscalationHistory
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertEscalationId { get; set; }

    public int PreviousLevel { get; set; }

    public int CurrentLevel { get; set; }

    public string Action { get; set; } = string.Empty;

    public string? PerformedBy { get; set; }

    public string? Notes { get; set; }

    public DateTime PerformedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignAlertEscalation? Escalation { get; set; }
}
