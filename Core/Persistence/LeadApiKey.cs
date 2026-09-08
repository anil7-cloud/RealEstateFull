namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadApiKey
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string ApiKey { get; set; } = string.Empty;

    public string SecretKey { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string PermissionsJson { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public bool IsRevoked { get; set; }

    public int? CreatedByUserId { get; set; }

    public int RequestCount { get; set; }

    public int DailyLimit { get; set; }

    public int MonthlyLimit { get; set; }

    public DateTime? LastUsedAt { get; set; }

    public string LastIpAddress { get; set; } = string.Empty;

    public DateTime? ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
