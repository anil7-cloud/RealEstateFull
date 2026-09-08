namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;

public class PropertyRecommendationDto
{
    public int CustomerId { get; set; }

    public string PropertyTitle { get; set; } = "";

    public decimal Price { get; set; }

    public int MatchScore { get; set; }

    public string Reason { get; set; } = "";
}
