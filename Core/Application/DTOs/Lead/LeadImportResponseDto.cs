namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadImportResponseDto
{
    public int TotalRecords { get; set; }

    public int SuccessCount { get; set; }

    public int FailedCount { get; set; }

    public List<string> Errors { get; set; } = new();

    public DateTime ImportedAt { get; set; } = DateTime.UtcNow;
}
