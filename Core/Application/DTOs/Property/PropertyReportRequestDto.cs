namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyReportRequestDto
{
    public int? PropertyId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? ReportType { get; set; }

    public bool IncludeDetails { get; set; } = true;
}
