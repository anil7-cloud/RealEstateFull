namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiPricingRequestDto
{
    public int PropertyId { get; set; }

    public decimal CurrentPrice { get; set; }

    public decimal SuggestedPrice { get; set; }

    public decimal PriceChangePercentage { get; set; }

    public string PricingReason { get; set; } = string.Empty;

    public decimal ConfidenceScore { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
