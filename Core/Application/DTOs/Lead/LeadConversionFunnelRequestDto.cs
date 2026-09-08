namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadConversionFunnelRequestDto
{
    public string StageName { get; set; } = string.Empty;

    public int LeadCount { get; set; }

    public decimal ConversionRate { get; set; }

    public decimal AverageDuration { get; set; }

    public int Order { get; set; }
}
