namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiValuationRequestDto
{
    public int PropertyId { get; set; }

    public decimal EstimatedValue { get; set; }

    public decimal MarketScore { get; set; }

    public string ValuationReason { get; set; } = string.Empty;

    public decimal ConfidenceScore { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
