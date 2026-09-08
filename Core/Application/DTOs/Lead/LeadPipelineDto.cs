namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadPipelineDto
{
    public string Status { get; set; } = string.Empty;

    public int Count { get; set; }

    public decimal TotalValue { get; set; }

    public decimal ConversionRate { get; set; }
}
