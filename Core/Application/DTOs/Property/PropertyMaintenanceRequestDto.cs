namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyMaintenanceRequestDto
{
    public int PropertyId { get; set; }

    public string MaintenanceType { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Cost { get; set; }

    public DateTime MaintenanceDate { get; set; } = DateTime.UtcNow;

    public string Status { get; set; } = string.Empty;
}
