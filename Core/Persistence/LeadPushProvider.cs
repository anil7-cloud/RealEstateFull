namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushProvider
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string ProviderType { get; set; } = string.Empty;

    public string ApiKey { get; set; } = string.Empty;

    public string ApiSecret { get; set; } = string.Empty;

    public string BaseUrl { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public int Priority { get; set; }

    public int DailyLimit { get; set; }

    public int SentToday { get; set; }

    public DateTime? LastSentAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public string Code { get; set; } = string.Empty;
    public string? ServerKey { get; set; }
    public string? SenderId { get; set; }
    public string? ProjectId { get; set; }
    public string? Endpoint { get; set; }
    public bool IsDefault { get; set; }
    public string? Notes { get; set; }
}
