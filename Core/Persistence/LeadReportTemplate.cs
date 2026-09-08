namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadReportTemplate
{
    public int Id { get; set; }

    public string TemplateName { get; set; } = string.Empty;

    public string TemplateCode { get; set; } = string.Empty;

    public string ReportType { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string LayoutJson { get; set; } = string.Empty;

    public string FiltersJson { get; set; } = string.Empty;

    public string ColumnsJson { get; set; } = string.Empty;

    public string ExportFormat { get; set; } = "PDF";

    public bool IsDefault { get; set; }

    public bool IsPublic { get; set; }

    public int? CreatedByUserId { get; set; }

    public int UsageCount { get; set; }

    public DateTime? LastUsedAt { get; set; }

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
