namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaign
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int? LeadPushNotificationTemplateId { get; set; }

    public string AudienceType { get; set; } = "All";

    public string AudienceFilterJson { get; set; } = string.Empty;

    public bool SendImmediately { get; set; }

    public DateTime? ScheduledAt { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public int TotalRecipients { get; set; }

    public int SentCount { get; set; }

    public int DeliveredCount { get; set; }

    public int FailedCount { get; set; }

    public int ClickCount { get; set; }

    public decimal ClickRate { get; set; }

    public string Status { get; set; } = "Draft";

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
