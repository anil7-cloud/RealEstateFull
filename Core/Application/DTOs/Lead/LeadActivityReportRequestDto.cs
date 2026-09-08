namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadActivityReportRequestDto
{
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? UserId { get; set; }

    public string? ActivityType { get; set; }

    public bool IncludeCompleted { get; set; } = true;
}
