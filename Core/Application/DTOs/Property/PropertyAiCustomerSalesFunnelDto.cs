namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesFunnelDto
{
    public int UserId { get; set; }

    public int LeadCount { get; set; }

    public int QualifiedLeadCount { get; set; }

    public int OfferCount { get; set; }

    public int SaleCount { get; set; }

    public decimal FunnelConversionRate { get; set; }

    public string FunnelAnalysis { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
