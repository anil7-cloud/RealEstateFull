namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesLeadForecastMetricsDto
{
    public int UserId { get; set; }

    public int TotalLeads { get; set; }

    public int QualifiedLeads { get; set; }

    public int ConvertedLeads { get; set; }

    public decimal ConversionRate { get; set; }

    public decimal AverageLeadScore { get; set; }

    public decimal RevenuePrediction { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
