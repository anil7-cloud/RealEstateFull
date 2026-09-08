namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushBounce
{
    public int Id { get; set; }

    public int LeadPushTaskId { get; set; }

    public string Recipient { get; set; } = string.Empty;

    public string BounceType { get; set; } = string.Empty;

    public string ErrorCode { get; set; } = string.Empty;

    public string ErrorMessage { get; set; } = string.Empty;

    public bool IsPermanent { get; set; }

    public DateTime BouncedAt { get; set; } = DateTime.UtcNow;

    public LeadPushTask? Task { get; set; }
}
