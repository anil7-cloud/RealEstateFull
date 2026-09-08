namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyPerformanceRequestDto
{
    public int? PropertyId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool IncludeViews { get; set; } = true;

    public bool IncludeLeads { get; set; } = true;

    public bool IncludeSales { get; set; } = true;

    public bool IncludeRevenue { get; set; } = true;
}
