namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignSchedule
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string TimeZone { get; set; } = "UTC";

    public bool RepeatEnabled { get; set; }

    public string? RepeatRule { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaign? Campaign { get; set; }
}
