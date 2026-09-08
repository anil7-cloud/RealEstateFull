namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiMatchingRequestDto
{
    public int PropertyId { get; set; }

    public int LeadId { get; set; }

    public decimal MatchScore { get; set; }

    public string MatchingReason { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
