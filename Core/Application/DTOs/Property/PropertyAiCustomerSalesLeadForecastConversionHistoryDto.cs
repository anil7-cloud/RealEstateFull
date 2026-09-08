namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesLeadForecastConversionHistoryDto
{
    public int UserId { get; set; }

    public decimal ConversionScore { get; set; }

    public string ConversionLevel { get; set; } = string.Empty;

    public string ConversionAnalysis { get; set; } = string.Empty;

    public List<string> Recommendations { get; set; } = new();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
