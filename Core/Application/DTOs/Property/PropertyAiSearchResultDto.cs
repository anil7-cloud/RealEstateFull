namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiSearchResultDto
{
    public int PropertyId { get; set; }

    public string Title { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public decimal MatchScore { get; set; }

    public string MatchReason { get; set; } = string.Empty;

    public string Recommendation { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;
}
