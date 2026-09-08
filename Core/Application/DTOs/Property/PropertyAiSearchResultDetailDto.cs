namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiSearchResultDetailDto
{
    public int PropertyId { get; set; }

    public string Title { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string Description { get; set; } = string.Empty;

    public decimal MatchScore { get; set; }

    public string MatchReason { get; set; } = string.Empty;

    public List<string> Recommendations { get; set; } = new();
}
