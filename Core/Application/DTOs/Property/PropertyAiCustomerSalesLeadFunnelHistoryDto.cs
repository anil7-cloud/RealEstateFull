namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesLeadFunnelHistoryDto
{
    public int UserId { get; set; }

    public int ViewStage { get; set; }

    public int ContactStage { get; set; }

    public int OfferStage { get; set; }

    public int ConversionStage { get; set; }

    public decimal FunnelConversionRate { get; set; }

    public string FunnelAnalysis { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
