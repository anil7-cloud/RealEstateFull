namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadBulkDeleteRequestDto
{
    public List<int> LeadIds { get; set; } = new();

    public string Reason { get; set; } = string.Empty;

    public int DeletedByUserId { get; set; }

    public bool PermanentDelete { get; set; }
}
