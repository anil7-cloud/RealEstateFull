namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesAnalyticsHistoryDto
{
    public int UserId { get; set; }

    public int TotalOffers { get; set; }

    public int AcceptedOffers { get; set; }

    public int RejectedOffers { get; set; }

    public decimal ConversionRate { get; set; }

    public decimal ExpectedRevenue { get; set; }

    public string SalesAnalysis { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
