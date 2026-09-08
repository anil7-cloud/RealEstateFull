namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadSourceRequestDto
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}
