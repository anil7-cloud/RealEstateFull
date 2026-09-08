namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiRecommendationSummaryDto
{
    public int PropertyId { get; set; }

    public string Title { get; set; } = string.Empty;

    public decimal Score { get; set; }

    public string Summary { get; set; } = string.Empty;

    public string MainReason { get; set; } = string.Empty;

    public List<string> Highlights { get; set; } = new();

    public DateTime CreatedAt { get; set; }
}
