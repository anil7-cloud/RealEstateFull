namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadExportJob
{
    public int Id { get; set; }

    public string JobName { get; set; } = string.Empty;

    public string ExportType { get; set; } = "Excel";

    public string ReportType { get; set; } = string.Empty;

    public string FiltersJson { get; set; } = string.Empty;

    public string FileName { get; set; } = string.Empty;

    public string FilePath { get; set; } = string.Empty;

    public long FileSize { get; set; }

    public string MimeType { get; set; } = string.Empty;

    public int? RequestedByUserId { get; set; }

    public string Status { get; set; } = "Pending";

    public int ProgressPercentage { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string ErrorMessage { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
