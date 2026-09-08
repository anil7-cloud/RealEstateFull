namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadAiPredictionResponseDto
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public string PredictionType { get; set; } = string.Empty;

    public decimal Probability { get; set; }

    public string PredictionResult { get; set; } = string.Empty;

    public string Explanation { get; set; } = string.Empty;

    public DateTime PredictedAt { get; set; }
}
