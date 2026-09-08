namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadBulkDeleteResponseDto
{
    public int DeletedCount { get; set; }

    public List<int> DeletedLeadIds { get; set; } = new();

    public string Message { get; set; } = "Leads deleted successfully";

    public DateTime DeletedAt { get; set; } = DateTime.UtcNow;
}
