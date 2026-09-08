namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadMatchResponseDto
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int PropertyId { get; set; }

    public decimal MatchScore { get; set; }

    public string Reason { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
