namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiNotificationHistoryDto
{
    public int UserId { get; set; }

    public int PropertyId { get; set; }

    public string NotificationType { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public bool IsRead { get; set; }

    public DateTime SentAt { get; set; } = DateTime.UtcNow;
}
