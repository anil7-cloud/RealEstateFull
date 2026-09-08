namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiInsightResultDto
{
    public int PropertyId { get; set; }

    public string InsightType { get; set; } = string.Empty;

    public string InsightText { get; set; } = string.Empty;

    public decimal ImpactScore { get; set; }

    public string Recommendation { get; set; } = string.Empty;
}
