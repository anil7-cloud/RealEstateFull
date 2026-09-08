namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiRecommendationResultDto
{
    public int PropertyId { get; set; }

    public string Title { get; set; } = string.Empty;

    public decimal Score { get; set; }

    public string Reason { get; set; } = string.Empty;

    public decimal EstimatedValue { get; set; }

    public string RecommendationType { get; set; } = string.Empty;
}
