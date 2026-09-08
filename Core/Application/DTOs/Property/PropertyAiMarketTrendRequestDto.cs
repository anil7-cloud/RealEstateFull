namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiMarketTrendRequestDto
{
    public int PropertyId { get; set; }

    public string TrendType { get; set; } = string.Empty;

    public decimal CurrentMarketValue { get; set; }

    public decimal PreviousMarketValue { get; set; }

    public decimal ChangeRate { get; set; }

    public string AnalysisPeriod { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
