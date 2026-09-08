namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadActivityReportResponseDto
{
    public int TotalActivities { get; set; }

    public int CallCount { get; set; }

    public int MeetingCount { get; set; }

    public int EmailCount { get; set; }

    public int MessageCount { get; set; }

    public int CompletedCount { get; set; }

    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
}
