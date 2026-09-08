namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadSourceDetailRequestDto
{
    public int LeadSourceId { get; set; }

    public string DetailName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}
