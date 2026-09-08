namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAlertRequestDto
{
    public int UserId { get; set; }

    public string AlertName { get; set; } = string.Empty;

    public string? City { get; set; }

    public string? District { get; set; }

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public int? PropertyTypeId { get; set; }

    public bool IsActive { get; set; } = true;
}
