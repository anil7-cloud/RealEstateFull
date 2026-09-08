namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerJourneyDto
{
    public int UserId { get; set; }

    public int PropertyId { get; set; }

    public string JourneyStage { get; set; } = string.Empty;

    public string StageDescription { get; set; } = string.Empty;

    public decimal ConversionProbability { get; set; }

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
