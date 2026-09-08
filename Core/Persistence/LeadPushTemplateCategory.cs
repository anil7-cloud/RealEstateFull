namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushTemplateCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Icon { get; set; } = string.Empty;

    public string Color { get; set; } = "#2196F3";

    public int DisplayOrder { get; set; }

    public bool IsDefault { get; set; }

    public bool IsActive { get; set; } = true;

    public int TemplateCount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
