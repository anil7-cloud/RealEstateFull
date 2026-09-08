namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiScoreRequestDto
{
    public int PropertyId { get; set; }

    public decimal Score { get; set; }

    public string ScoreType { get; set; } = string.Empty;

    public string Explanation { get; set; } = string.Empty;

    public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;
}
