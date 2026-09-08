namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushAudit
{
    public int Id { get; set; }

    public string EntityName { get; set; } = string.Empty;

    public int EntityId { get; set; }

    public string Action { get; set; } = string.Empty;

    public int? UserId { get; set; }

    public string OldValues { get; set; } = string.Empty;

    public string NewValues { get; set; } = string.Empty;

    public string IpAddress { get; set; } = string.Empty;

    public string UserAgent { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
