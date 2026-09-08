namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadUpdateResponseDto
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Message { get; set; } = "Lead updated successfully";

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
