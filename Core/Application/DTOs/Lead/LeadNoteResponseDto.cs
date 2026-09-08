namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadNoteResponseDto
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public string Note { get; set; } = string.Empty;

    public int? UserId { get; set; }

    public string? UserName { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
