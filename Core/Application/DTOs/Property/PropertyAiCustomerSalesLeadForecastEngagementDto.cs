namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesLeadForecastEngagementDto
{
    public int UserId { get; set; }

    public int InteractionCount { get; set; }

    public int ActiveDays { get; set; }

    public decimal EngagementScore { get; set; }

    public string EngagementLevel { get; set; } = string.Empty;

    public string EngagementSummary { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
