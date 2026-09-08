namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadDocumentDto
{
    public int LeadId { get; set; }

    public string FileName { get; set; } = string.Empty;

    public string FilePath { get; set; } = string.Empty;

    public string DocumentType { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}
