namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesLeadStrategyHistoryDto
{
    public int UserId { get; set; }

    public string StrategyType { get; set; } = string.Empty;

    public string StrategyDescription { get; set; } = string.Empty;

    public decimal SuccessProbability { get; set; }

    public string Recommendation { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
