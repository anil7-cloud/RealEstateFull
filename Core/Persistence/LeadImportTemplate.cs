namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadImportTemplate
{
    public int Id { get; set; }

    public string TemplateName { get; set; } = string.Empty;

    public string TemplateCode { get; set; } = string.Empty;

    public string ImportType { get; set; } = "Excel";

    public string Description { get; set; } = string.Empty;

    public string MappingJson { get; set; } = string.Empty;

    public string RequiredColumnsJson { get; set; } = string.Empty;

    public string OptionalColumnsJson { get; set; } = string.Empty;

    public bool HasHeaderRow { get; set; } = true;

    public char Delimiter { get; set; } = ',';

    public string DateFormat { get; set; } = "yyyy-MM-dd";

    public string DefaultValuesJson { get; set; } = string.Empty;

    public bool IsDefault { get; set; }

    public bool IsPublic { get; set; }

    public int UsageCount { get; set; }

    public DateTime? LastUsedAt { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
