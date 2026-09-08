namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadConversionDto
{
    public int LeadId { get; set; }

    public int? PropertyId { get; set; }

    public decimal Value { get; set; }

    public string ConversionType { get; set; } = string.Empty;

    public DateTime ConversionDate { get; set; } = DateTime.UtcNow;

    public string Notes { get; set; } = string.Empty;
}

