namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadApiClient
{
    public int Id { get; set; }

    public string ClientName { get; set; } = string.Empty;

    public string ClientCode { get; set; } = string.Empty;

    public string ContactPerson { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Company { get; set; } = string.Empty;

    public string Website { get; set; } = string.Empty;

    public string BaseUrl { get; set; } = string.Empty;

    public string AllowedIpAddresses { get; set; } = string.Empty;

    public string AllowedOrigins { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public bool IsBlocked { get; set; }

    public int DailyRequestLimit { get; set; }

    public int MonthlyRequestLimit { get; set; }

    public int TotalRequests { get; set; }

    public DateTime? LastRequestAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
