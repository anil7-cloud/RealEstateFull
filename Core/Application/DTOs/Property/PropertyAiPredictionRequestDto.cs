namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiPredictionRequestDto
{
    public int PropertyId { get; set; }

    public string PredictionType { get; set; } = string.Empty;

    public decimal PredictedValue { get; set; }

    public decimal ConfidenceScore { get; set; }

    public string Explanation { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
