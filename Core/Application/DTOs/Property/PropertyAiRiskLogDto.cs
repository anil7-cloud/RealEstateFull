namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiRiskLogDto
{
    public int PropertyId { get; set; }

    public string RiskType { get; set; } = string.Empty;

    public decimal RiskScore { get; set; }

    public string RiskDescription { get; set; } = string.Empty;

    public string Recommendation { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
