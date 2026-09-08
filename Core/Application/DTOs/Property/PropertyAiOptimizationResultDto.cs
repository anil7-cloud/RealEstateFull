namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiOptimizationResultDto
{
    public int PropertyId { get; set; }

    public string OptimizationType { get; set; } = string.Empty;

    public string Recommendation { get; set; } = string.Empty;

    public decimal ImprovementScore { get; set; }

    public DateTime CreatedAt { get; set; }
}
