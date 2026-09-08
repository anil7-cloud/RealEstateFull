namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerRecommendationDto
{
    public int UserId { get; set; }

    public int PropertyId { get; set; }

    public decimal RecommendationScore { get; set; }

    public string RecommendationReason { get; set; } = string.Empty;

    public string RecommendationType { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
