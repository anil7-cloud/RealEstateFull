namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesLeadPredictionDto
{
    public int UserId { get; set; }

    public decimal LeadConversionProbability { get; set; }

    public string PredictionType { get; set; } = string.Empty;

    public string PredictionResult { get; set; } = string.Empty;

    public decimal ConfidenceScore { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
