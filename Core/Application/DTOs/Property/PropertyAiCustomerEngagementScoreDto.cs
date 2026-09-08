namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerEngagementScoreDto
{
    public int UserId { get; set; }

    public decimal EngagementScore { get; set; }

    public string EngagementLevel { get; set; } = string.Empty;

    public string EngagementAnalysis { get; set; } = string.Empty;

    public List<string> Recommendations { get; set; } = new();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
