namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadReportResponseDto
{
    public int TotalCount { get; set; }

    public int NewCount { get; set; }

    public int ActiveCount { get; set; }

    public int ConvertedCount { get; set; }

    public int LostCount { get; set; }

    public decimal TotalRevenue { get; set; }

    public decimal ConversionRate { get; set; }

    public DateTime GeneratedAt { get; set; }
}
