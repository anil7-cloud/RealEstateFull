namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadImportRequestDto
{
    public string FileName { get; set; } = string.Empty;

    public string FilePath { get; set; } = string.Empty;

    public string Format { get; set; } = "CSV";

    public bool ValidateBeforeImport { get; set; } = true;

    public int ImportedByUserId { get; set; }
}
