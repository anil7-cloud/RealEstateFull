namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadSourceDetailResponseDto
{
    public int Id { get; set; }

    public int LeadSourceId { get; set; }

    public string DetailName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int LeadCount { get; set; }

    public DateTime CreatedAt { get; set; }
}
