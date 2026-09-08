namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiDecisionResultDto
{
    public int PropertyId { get; set; }

    public string DecisionType { get; set; } = string.Empty;

    public string DecisionResult { get; set; } = string.Empty;

    public decimal ConfidenceScore { get; set; }

    public string Explanation { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
