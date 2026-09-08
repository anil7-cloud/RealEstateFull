namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadCreateResponseDto
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Message { get; set; } = "Lead created successfully";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
