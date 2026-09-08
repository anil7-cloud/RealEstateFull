namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiComparisonResultDto
{
    public List<int> PropertyIds { get; set; } = new();

    public string ComparisonSummary { get; set; } = string.Empty;

    public List<string> Advantages { get; set; } = new();

    public List<string> Disadvantages { get; set; } = new();

    public int RecommendedPropertyId { get; set; }

    public string RecommendationReason { get; set; } = string.Empty;
}
