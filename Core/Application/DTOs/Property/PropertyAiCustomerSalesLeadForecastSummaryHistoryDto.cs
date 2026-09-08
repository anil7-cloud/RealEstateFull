namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesLeadForecastSummaryHistoryDto
{
    public int UserId { get; set; }

    public string SummaryTitle { get; set; } = string.Empty;

    public string SummaryContent { get; set; } = string.Empty;

    public decimal SummaryScore { get; set; }

    public decimal RevenuePrediction { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
