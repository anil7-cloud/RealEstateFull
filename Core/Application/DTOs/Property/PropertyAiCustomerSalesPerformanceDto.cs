namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesPerformanceDto
{
    public int UserId { get; set; }

    public int TotalSales { get; set; }

    public decimal TotalRevenue { get; set; }

    public decimal AverageSaleValue { get; set; }

    public decimal SalesScore { get; set; }

    public string PerformanceAnalysis { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
