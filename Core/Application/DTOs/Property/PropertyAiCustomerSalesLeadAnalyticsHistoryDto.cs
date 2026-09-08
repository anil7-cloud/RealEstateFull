namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesLeadAnalyticsHistoryDto
{
    public int UserId { get; set; }

    public int TotalLeads { get; set; }

    public int QualifiedLeads { get; set; }

    public int ConvertedLeads { get; set; }

    public decimal ConversionRate { get; set; }

    public decimal AverageLeadScore { get; set; }

    public string AnalyticsSummary { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
