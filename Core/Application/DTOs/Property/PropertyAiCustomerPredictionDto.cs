namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerPredictionDto
{
    public int UserId { get; set; }

    public decimal PurchaseProbability { get; set; }

    public string PredictionType { get; set; } = string.Empty;

    public string PredictionResult { get; set; } = string.Empty;

    public decimal ConfidenceScore { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
