namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushProviderHealth
{
    public int Id { get; set; }

    public int LeadPushProviderId { get; set; }

    public bool IsHealthy { get; set; }

    public int SuccessCount { get; set; }

    public int FailureCount { get; set; }

    public double AverageResponseTimeMs { get; set; }

    public DateTime? LastSuccessAt { get; set; }

    public DateTime? LastFailureAt { get; set; }

    public string LastError { get; set; } = string.Empty;

    public DateTime CheckedAt { get; set; } = DateTime.UtcNow;

    public LeadPushProvider? Provider { get; set; }
}
