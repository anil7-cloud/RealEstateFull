namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyContactRequestDto
{
    public int PropertyId { get; set; }

    public string ContactName { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string ContactType { get; set; } = string.Empty;
}
