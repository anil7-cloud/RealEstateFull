namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class CreateLeadDto
{
    public string FullName { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Source { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public int? PropertyId { get; set; }
}
