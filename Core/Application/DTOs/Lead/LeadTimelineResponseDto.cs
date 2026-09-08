namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadTimelineResponseDto
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public string EventType { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int? UserId { get; set; }

    public string? UserName { get; set; }

    public DateTime EventDate { get; set; }

    public DateTime CreatedAt { get; set; }
}
