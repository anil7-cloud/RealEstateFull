namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiMarketAnalysisResultDto
{
    public int PropertyId { get; set; }

    public decimal CurrentPrice { get; set; }

    public decimal MarketAveragePrice { get; set; }

    public decimal PriceDifference { get; set; }

    public string MarketCondition { get; set; } = string.Empty;

    public string AnalysisResult { get; set; } = string.Empty;

    public List<string> Recommendations { get; set; } = new();

    public DateTime CreatedAt { get; set; }
}
