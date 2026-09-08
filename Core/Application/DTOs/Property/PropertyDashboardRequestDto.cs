namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyDashboardRequestDto
{
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? UserId { get; set; }

    public bool IncludeStatistics { get; set; } = true;

    public bool IncludeCharts { get; set; } = true;
}
