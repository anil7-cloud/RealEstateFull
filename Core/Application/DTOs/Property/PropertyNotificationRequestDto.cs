namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyNotificationRequestDto
{
    public int PropertyId { get; set; }

    public int? UserId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string NotificationType { get; set; } = string.Empty;

    public bool IsRead { get; set; }
}
