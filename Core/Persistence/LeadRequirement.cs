namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadRequirement
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public string RequirementName { get; set; } = string.Empty;

    public string RequirementValue { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public bool IsMandatory { get; set; }

    public int Priority { get; set; }

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
