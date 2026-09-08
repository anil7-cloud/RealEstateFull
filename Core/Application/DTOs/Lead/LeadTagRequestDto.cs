namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadTagRequestDto
{
    public int LeadId { get; set; }

    public string TagName { get; set; } = string.Empty;

    public string Color { get; set; } = string.Empty;
}
