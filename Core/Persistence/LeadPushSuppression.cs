namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushSuppression
{
    public int Id { get; set; }

    public string Recipient { get; set; } = string.Empty;

    public string Channel { get; set; } = string.Empty;

    public string Reason { get; set; } = string.Empty;

    public bool IsPermanent { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}
