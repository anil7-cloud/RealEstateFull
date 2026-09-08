namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiAnalysisResultDto
{
    public int PropertyId { get; set; }

    public string AnalysisType { get; set; } = string.Empty;

    public string AnalysisResult { get; set; } = string.Empty;

    public decimal ConfidenceScore { get; set; }

    public string Recommendation { get; set; } = string.Empty;
}
