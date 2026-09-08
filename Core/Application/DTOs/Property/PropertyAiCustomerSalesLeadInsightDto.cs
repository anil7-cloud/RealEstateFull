namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesLeadInsightDto
{
    public int UserId { get; set; }

    public string InsightType { get; set; } = string.Empty;

    public string InsightText { get; set; } = string.Empty;

    public decimal LeadImpactScore { get; set; }

    public string Recommendation { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
