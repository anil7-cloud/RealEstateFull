namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyBulkDeleteRequestDto
{
    public List<int> PropertyIds { get; set; } = new();

    public string Reason { get; set; } = string.Empty;

    public int DeletedByUserId { get; set; }

    public bool PermanentDelete { get; set; }
}
