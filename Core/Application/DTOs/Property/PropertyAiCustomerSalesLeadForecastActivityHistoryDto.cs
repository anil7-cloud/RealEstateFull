namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesLeadForecastActivityHistoryDto
{
    public int UserId { get; set; }

    public string ActivityType { get; set; } = string.Empty;

    public string ActivityDescription { get; set; } = string.Empty;

    public int ActivityCount { get; set; }

    public decimal ActivityScore { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
