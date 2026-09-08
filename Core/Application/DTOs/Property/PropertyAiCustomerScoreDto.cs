namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerScoreDto
{
    public int UserId { get; set; }

    public decimal CustomerScore { get; set; }

    public string ScoreType { get; set; } = string.Empty;

    public string Explanation { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
