namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class NotificationPreference
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public bool EmailEnabled { get; set; } = true;

    public bool SmsEnabled { get; set; } = false;

    public bool PushEnabled { get; set; } = true;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
