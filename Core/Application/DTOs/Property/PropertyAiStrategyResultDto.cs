namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiStrategyResultDto
{
    public int PropertyId { get; set; }

    public string StrategyType { get; set; } = string.Empty;

    public string Goal { get; set; } = string.Empty;

    public decimal TargetValue { get; set; }

    public string StrategyResult { get; set; } = string.Empty;

    public List<string> Recommendations { get; set; } = new();

    public DateTime CreatedAt { get; set; }
}
