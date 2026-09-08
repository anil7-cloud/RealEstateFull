namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyMetricsRequestDto
{
    public int? PropertyId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool IncludePerformance { get; set; } = true;

    public bool IncludeConversion { get; set; } = true;

    public bool IncludeFinancials { get; set; } = true;
}
