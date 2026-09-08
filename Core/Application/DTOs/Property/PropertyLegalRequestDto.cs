namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyLegalRequestDto
{
    public int PropertyId { get; set; }

    public string DocumentType { get; set; } = string.Empty;

    public string DocumentNumber { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
