namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerAnalyticsDto
{
    public int UserId { get; set; }

    public int TotalViews { get; set; }

    public int TotalFavorites { get; set; }

    public int TotalOffers { get; set; }

    public int TotalContacts { get; set; }

    public decimal EngagementScore { get; set; }

    public string AnalyticsSummary { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
