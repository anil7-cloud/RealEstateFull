namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiReportRequestDto
{
    public int PropertyId { get; set; }

    public string ReportType { get; set; } = string.Empty;

    public string ReportContent { get; set; } = string.Empty;

    public decimal ConfidenceScore { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
