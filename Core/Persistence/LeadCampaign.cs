namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadCampaign
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    // Google Ads, Facebook, Instagram, TikTok...
    public string Platform { get; set; } = string.Empty;

    public string CampaignType { get; set; } = string.Empty;

    public decimal Budget { get; set; }

    public decimal SpentAmount { get; set; }

    public int Clicks { get; set; }

    public int Impressions { get; set; }

    public int Leads { get; set; }

    public int Conversions { get; set; }

    public decimal Revenue { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    // Draft, Active, Paused, Completed
    public string Status { get; set; } = "Draft";

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
