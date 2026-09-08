namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadReport
{
    public int Id { get; set; }

    public string ReportName { get; set; } = string.Empty;

    public string ReportType { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string FiltersJson { get; set; } = string.Empty;

    public string ColumnsJson { get; set; } = string.Empty;

    public string SortBy { get; set; } = string.Empty;

    public bool SortDescending { get; set; }

    public int? CreatedByUserId { get; set; }

    public bool IsPublic { get; set; }

    public bool IsScheduled { get; set; }

    public string ScheduleCron { get; set; } = string.Empty;

    public DateTime? LastGeneratedAt { get; set; }

    public DateTime? NextRunAt { get; set; }

    public string ExportFormat { get; set; } = "PDF";

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
