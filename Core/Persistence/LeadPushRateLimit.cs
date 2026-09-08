namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushRateLimit
{
    public int Id { get; set; }

    public string ProviderName { get; set; } = string.Empty;

    public string Channel { get; set; } = string.Empty;

    public int MaxRequestsPerMinute { get; set; }

    public int MaxRequestsPerHour { get; set; }

    public int MaxRequestsPerDay { get; set; }

    public int CurrentMinuteRequests { get; set; }

    public int CurrentHourRequests { get; set; }

    public int CurrentDayRequests { get; set; }

    public DateTime WindowStartedAt { get; set; }

    public bool IsEnabled { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
