namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadOpportunityResponseDto
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public string OpportunityName { get; set; } = string.Empty;

    public decimal EstimatedValue { get; set; }

    public string Stage { get; set; } = string.Empty;

    public decimal Probability { get; set; }

    public DateTime ExpectedCloseDate { get; set; }

    public DateTime CreatedAt { get; set; }
}
