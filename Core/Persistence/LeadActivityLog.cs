namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadActivityLog
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int? UserId { get; set; }

    // Create, Update, Delete, Call, Email, SMS...
    public string Action { get; set; } = string.Empty;

    public string EntityName { get; set; } = string.Empty;

    public int? EntityId { get; set; }

    public string OldValue { get; set; } = string.Empty;

    public string NewValue { get; set; } = string.Empty;

    public string IpAddress { get; set; } = string.Empty;

    public string UserAgent { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public DateTime ActivityDate { get; set; } = DateTime.UtcNow;

    public bool IsSystem { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
