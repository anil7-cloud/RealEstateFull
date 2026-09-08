namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushDevice
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string DeviceToken { get; set; } = string.Empty;

    public string Platform { get; set; } = string.Empty; // Android, iOS, Web

    public string DeviceId { get; set; } = string.Empty;

    public string AppVersion { get; set; } = string.Empty;

    public string OperatingSystemVersion { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime? LastSeenAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public string DeviceName { get; set; } = string.Empty;
    public string? Manufacturer { get; set; }
    public string? Model { get; set; }
    public string? OperatingSystem { get; set; }
    public string? Language { get; set; }
    public string? TimeZone { get; set; }
    public bool NotificationsEnabled { get; set; } = true;
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastNotificationAt { get; set; }
}
