namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadSourceDetail
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public string SourceName { get; set; } = string.Empty;

    public string CampaignName { get; set; } = string.Empty;

    public string Medium { get; set; } = string.Empty;

    public string Channel { get; set; } = string.Empty;

    public string Referrer { get; set; } = string.Empty;

    public string Device { get; set; } = string.Empty;

    public string Browser { get; set; } = string.Empty;

    public string OperatingSystem { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string IpAddress { get; set; } = string.Empty;

    public decimal AcquisitionCost { get; set; }

    public bool IsConverted { get; set; }

    public DateTime SourceDate { get; set; } = DateTime.UtcNow;

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
