namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesLeadForecastAnalysisDto
{
    public int UserId { get; set; }

    public string AnalysisType { get; set; } = string.Empty;

    public string AnalysisResult { get; set; } = string.Empty;

    public decimal AnalysisScore { get; set; }

    public string Recommendation { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
