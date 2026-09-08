namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiPricingResultDto
{
    public int PropertyId { get; set; }

    public decimal CurrentPrice { get; set; }

    public decimal SuggestedPrice { get; set; }

    public decimal PriceDifference { get; set; }

    public decimal ConfidenceScore { get; set; }

    public string PricingReason { get; set; } = string.Empty;

    public List<string> Recommendations { get; set; } = new();

    public DateTime CreatedAt { get; set; }
}
