namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadConversionRequestDto
{
    public int LeadId { get; set; }

    public string ConversionType { get; set; } = string.Empty;

    public decimal DealValue { get; set; }

    public string Notes { get; set; } = string.Empty;

    public int ConvertedByUserId { get; set; }
}
