namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadScoreResponseDto
{
    public int LeadId { get; set; }

    public decimal Score { get; set; }

    public string ScoreLevel { get; set; } = string.Empty;

    public string Reason { get; set; } = string.Empty;

    public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;
}
