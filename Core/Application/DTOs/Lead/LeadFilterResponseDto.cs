namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadFilterResponseDto
{
    public List<LeadResponseDto> Leads { get; set; } = new();

    public int TotalCount { get; set; }

    public string? AppliedFilter { get; set; }

    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
}		
