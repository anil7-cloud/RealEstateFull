namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesPredictionDto
{
    public int UserId { get; set; }

    public decimal PurchaseProbability { get; set; }

    public decimal ExpectedRevenue { get; set; }

    public string SalesPrediction { get; set; } = string.Empty;

    public string PredictionReason { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
