namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAvailabilityRequestDto
{
    public int PropertyId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsAvailable { get; set; }

    public string Notes { get; set; } = string.Empty;
}
