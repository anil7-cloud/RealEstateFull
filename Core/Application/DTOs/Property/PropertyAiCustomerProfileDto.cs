namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerProfileDto
{
    public int UserId { get; set; }

    public string CustomerType { get; set; } = string.Empty;

    public string Preferences { get; set; } = string.Empty;

    public string BehaviorSummary { get; set; } = string.Empty;

    public decimal PurchaseProbability { get; set; }

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
