namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiMarketAnalysisRequestDto
{
    public int PropertyId { get; set; }

    public string AnalysisPeriod { get; set; } = string.Empty;

    public decimal CurrentPrice { get; set; }

    public decimal MarketAveragePrice { get; set; }

    public string MarketCondition { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;
}
