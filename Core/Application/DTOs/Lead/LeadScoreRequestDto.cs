namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadScoreRequestDto
{
    public int LeadId { get; set; }

    public decimal Score { get; set; }

    public string ScoreLevel { get; set; } = string.Empty;

    public string Reason { get; set; } = string.Empty;

    public int CalculatedByUserId { get; set; }
}
