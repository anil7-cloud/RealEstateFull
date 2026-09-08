namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiRecommendationDetailDto
{
    public int PropertyId { get; set; }

    public string Title { get; set; } = string.Empty;

    public decimal Score { get; set; }

    public string Reason { get; set; } = string.Empty;

    public List<string> Advantages { get; set; } = new();

    public List<string> Disadvantages { get; set; } = new();

    public string Recommendation { get; set; } = string.Empty;
}
