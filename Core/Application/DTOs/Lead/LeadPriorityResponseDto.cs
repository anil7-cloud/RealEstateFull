namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadPriorityResponseDto
{
    public int LeadId { get; set; }

    public string Priority { get; set; } = string.Empty;

    public string Reason { get; set; } = string.Empty;

    public DateTime UpdatedAt { get; set; }
}
