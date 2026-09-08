namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadPerformanceRequestDto
{
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? UserId { get; set; }

    public bool IncludeRevenue { get; set; } = true;

    public bool IncludeConversion { get; set; } = true;
}
