namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesLeadForecastPerformanceHistoryDto
{
    public int UserId { get; set; }

    public int TotalLeads { get; set; }

    public int ConvertedLeads { get; set; }

    public decimal ConversionRate { get; set; }

    public decimal PerformanceScore { get; set; }

    public string PerformanceAnalysis { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
