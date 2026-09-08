namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertEscalation
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertId { get; set; }

    public int EscalationLevel { get; set; }

    public string AssignedTo { get; set; } = string.Empty;

    public string Reason { get; set; } = string.Empty;

    public bool IsResolved { get; set; }

    public DateTime EscalatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? ResolvedAt { get; set; }

    public LeadPushCampaignAlert? Alert { get; set; }
}
