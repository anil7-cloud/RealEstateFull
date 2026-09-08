namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadDeleteResponseDto
{
    public int Id { get; set; }

    public string Message { get; set; } = "Lead deleted successfully";

    public DateTime DeletedAt { get; set; } = DateTime.UtcNow;
}
