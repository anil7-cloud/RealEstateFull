namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesLeadForecastReportHistoryDto
{
    public int UserId { get; set; }

    public string ReportTitle { get; set; } = string.Empty;

    public string ReportContent { get; set; } = string.Empty;

    public decimal ReportScore { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
