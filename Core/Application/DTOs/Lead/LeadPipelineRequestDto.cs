namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadPipelineRequestDto
{
    public string? Stage { get; set; }

    public int? UserId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool IncludeDetails { get; set; } = true;
}
