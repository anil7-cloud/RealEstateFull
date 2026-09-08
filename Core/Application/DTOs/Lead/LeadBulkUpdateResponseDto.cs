namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadBulkUpdateResponseDto
{
    public int UpdatedCount { get; set; }

    public List<int> UpdatedLeadIds { get; set; } = new();

    public string Message { get; set; } = "Leads updated successfully";

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
