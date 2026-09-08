namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerChurnHistoryDto
{
    public int UserId { get; set; }

    public decimal ChurnProbability { get; set; }

    public string ChurnLevel { get; set; } = string.Empty;

    public string ChurnReason { get; set; } = string.Empty;

    public List<string> Recommendations { get; set; } = new();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
