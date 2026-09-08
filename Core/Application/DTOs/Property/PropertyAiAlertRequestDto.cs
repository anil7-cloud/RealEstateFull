namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiAlertRequestDto
{
    public int PropertyId { get; set; }

    public int UserId { get; set; }

    public string AlertType { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public decimal ConfidenceScore { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
