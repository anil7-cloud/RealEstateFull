namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadEmailTemplate
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty;

    public string HtmlBody { get; set; } = string.Empty;

    public string PlainTextBody { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string VariablesJson { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string Language { get; set; } = "tr";

    public bool IsActive { get; set; } = true;

    public bool IsDefault { get; set; }

    public int UsageCount { get; set; }

    public DateTime? LastUsedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
