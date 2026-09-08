namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiRecommendationRequestDto
{
    public int UserId { get; set; }

    public string Preference { get; set; } = string.Empty;

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public string? Location { get; set; }

    public string? PropertyType { get; set; }

    public int RecommendationCount { get; set; } = 10;
}
