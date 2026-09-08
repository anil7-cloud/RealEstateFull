namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadBulkUpdateRequestDto
{
    public List<int> LeadIds { get; set; } = new();

    public string? Status { get; set; }

    public int? AssignedUserId { get; set; }

    public string? Priority { get; set; }

    public string? Note { get; set; }

    public int UpdatedByUserId { get; set; }
}
