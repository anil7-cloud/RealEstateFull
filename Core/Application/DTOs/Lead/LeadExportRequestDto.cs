namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadExportRequestDto
{
    public string Format { get; set; } = "CSV";

    public string? Filter { get; set; }

    public int? UserId { get; set; }

    public bool IncludeNotes { get; set; }

    public bool IncludeActivities { get; set; }

    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
}
