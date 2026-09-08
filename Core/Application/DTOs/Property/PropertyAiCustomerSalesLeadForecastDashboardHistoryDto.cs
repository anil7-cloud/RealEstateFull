namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesLeadForecastDashboardHistoryDto
{
    public int UserId { get; set; }

    public int TotalLeads { get; set; }

    public int ActiveLeads { get; set; }

    public int ConvertedLeads { get; set; }

    public decimal ConversionRate { get; set; }

    public decimal RevenuePrediction { get; set; }

    public string DashboardSummary { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
