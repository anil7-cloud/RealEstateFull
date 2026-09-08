namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesLeadMessageDto
{
    public int UserId { get; set; }

    public int PropertyId { get; set; }

    public string Message { get; set; } = string.Empty;

    public string SenderType { get; set; } = string.Empty;

    public bool IsRead { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
