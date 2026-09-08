namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadPipelineResponseDto
{
    public string Stage { get; set; } = string.Empty;

    public int Count { get; set; }

    public decimal TotalValue { get; set; }

    public decimal ConversionRate { get; set; }

    public List<LeadSummaryDto> Leads { get; set; } = new();
}
