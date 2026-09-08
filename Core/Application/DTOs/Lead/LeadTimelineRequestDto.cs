namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadTimelineRequestDto
{
    public int LeadId { get; set; }

    public string EventType { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int? UserId { get; set; }

    public DateTime EventDate { get; set; } = DateTime.UtcNow;
}
