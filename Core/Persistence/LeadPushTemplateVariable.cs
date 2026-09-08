namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushTemplateVariable
{
    public int Id { get; set; }

    public int LeadPushTemplateId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string DefaultValue { get; set; } = string.Empty;

    public bool IsRequired { get; set; }

    public int SortOrder { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushTemplate? Template { get; set; }

    public string Key { get; set; } = string.Empty;
    public string? ExampleValue { get; set; }
    public string? Category { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? UpdatedAt { get; set; }
}
