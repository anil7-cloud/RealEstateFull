namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiValuationLogDto
{
    public int PropertyId { get; set; }

    public decimal EstimatedValue { get; set; }

    public decimal MarketScore { get; set; }

    public decimal ConfidenceScore { get; set; }

    public string ValuationReason { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
