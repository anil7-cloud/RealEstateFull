namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushAudienceMember
{
    public int Id { get; set; }

    public int LeadPushAudienceId { get; set; }

    public int? UserId { get; set; }

    public int? LeadId { get; set; }

    public int? LeadPushDeviceId { get; set; }

    public string DeviceToken { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Platform { get; set; } = string.Empty;

    public string Language { get; set; } = "tr";

    public bool IsActive { get; set; } = true;

    public bool IsSubscribed { get; set; } = true;

    public DateTime? LastNotificationAt { get; set; }

    public int NotificationCount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
