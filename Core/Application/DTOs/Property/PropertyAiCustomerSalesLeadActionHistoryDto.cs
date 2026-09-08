namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesLeadActionHistoryDto
{
    public int UserId { get; set; }

    public string ActionType { get; set; } = string.Empty;

    public string ActionDescription { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }

    public string Result { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
