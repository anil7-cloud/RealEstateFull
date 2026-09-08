namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignChannel
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public string Channel { get; set; } = string.Empty; // Push, Email, SMS

    public bool IsEnabled { get; set; } = true;

    public int Priority { get; set; }

    public int DailyLimit { get; set; }

    public int HourlyLimit { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaign? Campaign { get; set; }
}
