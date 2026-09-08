namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadImportJob
{
    public int Id { get; set; }

    public string JobName { get; set; } = string.Empty;

    public string ImportType { get; set; } = "Excel";

    public string FileName { get; set; } = string.Empty;

    public string FilePath { get; set; } = string.Empty;

    public long FileSize { get; set; }

    public string MimeType { get; set; } = string.Empty;

    public int? RequestedByUserId { get; set; }

    public string Status { get; set; } = "Pending";

    public int TotalRows { get; set; }

    public int ProcessedRows { get; set; }

    public int SuccessfulRows { get; set; }

    public int FailedRows { get; set; }

    public int ProgressPercentage { get; set; }

    public string MappingJson { get; set; } = string.Empty;

    public string ErrorMessage { get; set; } = string.Empty;

    public string ErrorDetailsJson { get; set; } = string.Empty;

    public DateTime? StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
