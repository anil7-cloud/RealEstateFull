namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesCommunicationHistoryDto
{
    public int UserId { get; set; }

    public string CommunicationType { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string Direction { get; set; } = string.Empty;

    public bool IsSuccessful { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
