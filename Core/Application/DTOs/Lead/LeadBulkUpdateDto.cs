namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadBulkUpdateDto
{
    public List<int> LeadIds { get; set; } = new();

    public string? Status { get; set; }

    public int? AssignedUserId { get; set; }

    public string? Source { get; set; }

    public string? Notes { get; set; }
}
