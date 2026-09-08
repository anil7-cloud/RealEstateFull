namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushResult
{
    public int Id { get; set; }

    public int LeadPushScheduleId { get; set; }

    public int TotalRecipients { get; set; }

    public int SuccessfulCount { get; set; }

    public int FailedCount { get; set; }

    public TimeSpan Duration { get; set; }

    public DateTime CompletedAt { get; set; }

    public string Summary { get; set; } = string.Empty;

    public LeadPushSchedule? Schedule { get; set; }
}
