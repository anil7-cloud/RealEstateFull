namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadSystemLog
{
    public int Id { get; set; }

    public string Level { get; set; } = "Information";

    public string Category { get; set; } = string.Empty;

    public string Source { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string Exception { get; set; } = string.Empty;

    public string StackTrace { get; set; } = string.Empty;

    public string RequestPath { get; set; } = string.Empty;

    public string HttpMethod { get; set; } = string.Empty;

    public int? StatusCode { get; set; }

    public int? UserId { get; set; }

    public int? LeadId { get; set; }

    public string IpAddress { get; set; } = string.Empty;

    public string MachineName { get; set; } = string.Empty;

    public string Environment { get; set; } = string.Empty;

    public bool IsResolved { get; set; }

    public DateTime? ResolvedAt { get; set; }

    public string ResolutionNotes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
