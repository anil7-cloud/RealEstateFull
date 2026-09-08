namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerFunnelAnalyticsHistoryDto
{
    public int UserId { get; set; }

    public int TotalViews { get; set; }

    public int TotalFavorites { get; set; }

    public int TotalContacts { get; set; }

    public int TotalOffers { get; set; }

    public int TotalPurchases { get; set; }

    public decimal ConversionRate { get; set; }

    public string FunnelAnalysis { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
