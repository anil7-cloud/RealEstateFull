namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadMeetingResponseDto
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public DateTime MeetingDate { get; set; }

    public int DurationMinutes { get; set; }

    public string Notes { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }

    public DateTime CreatedAt { get; set; }
}
