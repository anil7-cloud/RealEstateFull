namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyOwnershipRequestDto
{
    public int PropertyId { get; set; }

    public int OwnerId { get; set; }

    public string OwnershipType { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string Notes { get; set; } = string.Empty;
}
