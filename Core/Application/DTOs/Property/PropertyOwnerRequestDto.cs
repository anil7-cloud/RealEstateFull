namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyOwnerRequestDto
{
    public int PropertyId { get; set; }

    public string OwnerName { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;
}
