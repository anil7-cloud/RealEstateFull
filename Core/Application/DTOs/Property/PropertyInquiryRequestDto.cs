namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyInquiryRequestDto
{
    public int PropertyId { get; set; }

    public int? UserId { get; set; }

    public string Message { get; set; } = string.Empty;

    public string ContactPhone { get; set; } = string.Empty;

    public string ContactEmail { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
