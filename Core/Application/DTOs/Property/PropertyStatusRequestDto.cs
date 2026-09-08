namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyStatusRequestDto
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public int Order { get; set; }
}
