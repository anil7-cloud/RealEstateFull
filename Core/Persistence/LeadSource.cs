namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadSource
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    // Website, Sahibinden, Hepsiemlak, Facebook,
    // Instagram, Google Ads, Referral, Walk-In...
    public string SourceName { get; set; } = string.Empty;

    public string CampaignName { get; set; } = string.Empty;

    public string Medium { get; set; } = string.Empty;

    public string Channel { get; set; } = string.Empty;

    public decimal AcquisitionCost { get; set; }

    public decimal ExpectedRevenue { get; set; }

    public bool Converted { get; set; }

    public DateTime SourceDate { get; set; } = DateTime.UtcNow;

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
