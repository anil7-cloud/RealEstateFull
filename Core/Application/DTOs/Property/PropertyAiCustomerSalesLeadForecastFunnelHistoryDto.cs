namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesLeadForecastFunnelHistoryDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ViewStage { get; set; }

    public int ContactStage { get; set; }

    public int OfferStage { get; set; }

    public int ConversionStage { get; set; }

    public double FunnelConversionRate { get; set; }

    public string FunnelAnalysis { get; set; } = "";
}
