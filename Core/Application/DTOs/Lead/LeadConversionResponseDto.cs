namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadConversionResponseDto
{
    public int LeadId { get; set; }

    public bool IsConverted { get; set; }

    public string ConversionType { get; set; } = string.Empty;

    public decimal DealValue { get; set; }

    public string Message { get; set; } = "Lead converted successfully";

    public DateTime ConvertedAt { get; set; } = DateTime.UtcNow;
}
