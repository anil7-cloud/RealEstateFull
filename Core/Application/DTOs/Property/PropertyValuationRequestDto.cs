namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyValuationRequestDto
{
    public int PropertyId { get; set; }

    public decimal EstimatedValue { get; set; }

    public string ValuationType { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int ValuatedByUserId { get; set; }

    public DateTime ValuationDate { get; set; } = DateTime.UtcNow;
}
