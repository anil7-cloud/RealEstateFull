namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerFunnelHistoryDto
{
    public int UserId { get; set; }

    public int ViewStage { get; set; }

    public int FavoriteStage { get; set; }

    public int ContactStage { get; set; }

    public int OfferStage { get; set; }

    public int PurchaseStage { get; set; }

    public decimal FunnelConversionRate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
