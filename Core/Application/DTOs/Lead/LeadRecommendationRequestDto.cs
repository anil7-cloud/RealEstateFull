namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadRecommendationRequestDto
{
    public int LeadId { get; set; }

    public int PropertyId { get; set; }

    public string RecommendationType { get; set; } = string.Empty;

    public decimal Score { get; set; }

    public string Reason { get; set; } = string.Empty;
}
