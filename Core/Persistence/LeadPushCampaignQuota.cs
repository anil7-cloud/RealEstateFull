namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignQuota
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public int DailyQuota { get; set; }

    public int WeeklyQuota { get; set; }

    public int MonthlyQuota { get; set; }

    public int DailyUsed { get; set; }

    public int WeeklyUsed { get; set; }

    public int MonthlyUsed { get; set; }

    public bool ResetDaily { get; set; } = true;

    public bool ResetWeekly { get; set; } = true;

    public bool ResetMonthly { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public LeadPushCampaign? Campaign { get; set; }
}
