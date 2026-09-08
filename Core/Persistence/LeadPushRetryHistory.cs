namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushRetryHistory
{
    public int Id { get; set; }

    public int LeadPushTaskId { get; set; }

    public int AttemptNumber { get; set; }

    public DateTime AttemptedAt { get; set; } = DateTime.UtcNow;

    public bool IsSuccessful { get; set; }

    public int ResponseStatusCode { get; set; }

    public string ErrorCode { get; set; } = string.Empty;

    public string ErrorMessage { get; set; } = string.Empty;

    public TimeSpan Duration { get; set; }

    public LeadPushTask? Task { get; set; }
}
