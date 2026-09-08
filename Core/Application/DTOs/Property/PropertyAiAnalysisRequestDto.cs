namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiAnalysisRequestDto
{
    public int PropertyId { get; set; }

    public string AnalysisType { get; set; } = string.Empty;

    public string AnalysisResult { get; set; } = string.Empty;

    public decimal ConfidenceScore { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
