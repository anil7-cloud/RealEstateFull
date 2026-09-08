namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiUserRecommendationDto
{
    public int UserId { get; set; }

    public int PropertyId { get; set; }

    public decimal RecommendationScore { get; set; }

    public string RecommendationReason { get; set; } = string.Empty;

    public bool IsAccepted { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
