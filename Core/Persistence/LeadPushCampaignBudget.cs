namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignBudget
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public decimal TotalBudget { get; set; }

    public decimal SpentAmount { get; set; }

    public decimal RemainingBudget { get; set; }

    public decimal CostPerDelivery { get; set; }

    public decimal CostPerClick { get; set; }

    public decimal CostPerConversion { get; set; }

    public bool StopWhenBudgetExceeded { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public LeadPushCampaign? Campaign { get; set; }
}
