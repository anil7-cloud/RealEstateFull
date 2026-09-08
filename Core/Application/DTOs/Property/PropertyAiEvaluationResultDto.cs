namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiEvaluationResultDto
{
    public int PropertyId { get; set; }

    public string EvaluationType { get; set; } = string.Empty;

    public decimal Score { get; set; }

    public string EvaluationResult { get; set; } = string.Empty;

    public string Recommendation { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
