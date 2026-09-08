namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadNotificationRequestDto
{
    public int LeadId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string Channel { get; set; } = string.Empty;

    public int? UserId { get; set; }
}
