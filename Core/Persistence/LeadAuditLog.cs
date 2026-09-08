namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadAuditLog
{
    public int Id { get; set; }

    public int? LeadId { get; set; }

    public int? UserId { get; set; }

    public string Action { get; set; } = string.Empty;

    public string EntityName { get; set; } = string.Empty;

    public int? EntityId { get; set; }

    public string OldValuesJson { get; set; } = string.Empty;

    public string NewValuesJson { get; set; } = string.Empty;

    public string ChangedFieldsJson { get; set; } = string.Empty;

    public string IpAddress { get; set; } = string.Empty;

    public string UserAgent { get; set; } = string.Empty;

    public string Browser { get; set; } = string.Empty;

    public string Device { get; set; } = string.Empty;

    public string OperatingSystem { get; set; } = string.Empty;

    public bool IsSuccess { get; set; } = true;

    public string ErrorMessage { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
