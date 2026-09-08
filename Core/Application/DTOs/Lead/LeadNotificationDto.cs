namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadNotificationDto
{
    public int LeadId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string Channel { get; set; } = string.Empty;

    public bool IsRead { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
