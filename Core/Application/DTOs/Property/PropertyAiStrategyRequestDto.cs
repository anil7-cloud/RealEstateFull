namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiStrategyRequestDto
{
    public int PropertyId { get; set; }

    public string StrategyType { get; set; } = string.Empty;

    public string Goal { get; set; } = string.Empty;

    public decimal TargetValue { get; set; }

    public string Recommendation { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
