namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerBehaviorAnalysisDto
{
    public int UserId { get; set; }

    public int ViewCount { get; set; }

    public int FavoriteCount { get; set; }

    public int SearchCount { get; set; }

    public int ContactCount { get; set; }

    public string BehaviorAnalysis { get; set; } = string.Empty;

    public decimal InterestScore { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
