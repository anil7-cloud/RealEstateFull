namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiOpportunityResultDto
{
    public int PropertyId { get; set; }

    public string OpportunityType { get; set; } = string.Empty;

    public decimal OpportunityScore { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Recommendation { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
