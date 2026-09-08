namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyBulkUpdateRequestDto
{
    public List<int> PropertyIds { get; set; } = new();

    public decimal? Price { get; set; }

    public int? StatusId { get; set; }

    public int? PropertyTypeId { get; set; }

    public string? City { get; set; }

    public string? District { get; set; }

    public int UpdatedByUserId { get; set; }
}
