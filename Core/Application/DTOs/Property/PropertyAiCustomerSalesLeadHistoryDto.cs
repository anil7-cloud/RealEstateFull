namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesLeadHistoryDto
{
    public int UserId { get; set; }

    public int PropertyId { get; set; }

    public string LeadStatus { get; set; } = string.Empty;

    public decimal LeadScore { get; set; }

    public string LeadSource { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
