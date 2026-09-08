namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesLeadForecastHistoryDto
{
    public int UserId { get; set; }

    public decimal ExpectedConversionProbability { get; set; }

    public decimal ExpectedRevenue { get; set; }

    public string ForecastPeriod { get; set; } = string.Empty;

    public string ForecastAnalysis { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
