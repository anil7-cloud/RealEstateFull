namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushConversion
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public int UserId { get; set; }

    public string ConversionType { get; set; } = string.Empty;

    public decimal ConversionValue { get; set; }

    public string Currency { get; set; } = "TRY";

    public string ReferenceId { get; set; } = string.Empty;

    public DateTime ConvertedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaign? Campaign { get; set; }
}
