namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignScheduleException
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Reason { get; set; } = string.Empty;

    public bool IsPaused { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public LeadPushCampaign? Campaign { get; set; }
}
