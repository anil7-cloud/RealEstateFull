namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushTask
{
    public int Id { get; set; }

    public int LeadPushScheduleId { get; set; }

    public string Recipient { get; set; } = string.Empty;

    public string RecipientType { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public int RetryCount { get; set; }

    public string ErrorMessage { get; set; } = string.Empty;

    public DateTime? SentAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public LeadPushSchedule? Schedule { get; set; }
}
