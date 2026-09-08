namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadNotificationTemplate
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public string TitleTemplate { get; set; } = string.Empty;

    public string MessageTemplate { get; set; } = string.Empty;

    public string NotificationType { get; set; } = string.Empty;

    public string Priority { get; set; } = "Normal";

    public string Icon { get; set; } = string.Empty;

    public string Color { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public bool SendPush { get; set; }

    public bool SendEmail { get; set; }

    public bool SendSms { get; set; }

    public int DisplayDurationSeconds { get; set; }

    public string VariablesJson { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
