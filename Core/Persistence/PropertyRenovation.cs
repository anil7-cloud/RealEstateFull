namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyRenovation
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public string RenovationType { get; set; } = string.Empty;

    public string ContractorName { get; set; } = string.Empty;

    public decimal EstimatedCost { get; set; }

    public decimal ActualCost { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string Status { get; set; } = "Planned";

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
