namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiInsightLogDto
{
    public int PropertyId { get; set; }

    public string InsightType { get; set; } = string.Empty;

    public string InsightText { get; set; } = string.Empty;

    public decimal ImpactScore { get; set; }

    public string Recommendation { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
