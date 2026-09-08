namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadExportResponseDto
{
    public string FileName { get; set; } = string.Empty;

    public string FilePath { get; set; } = string.Empty;

    public int RecordCount { get; set; }

    public string Format { get; set; } = "CSV";

    public DateTime ExportedAt { get; set; } = DateTime.UtcNow;
}
