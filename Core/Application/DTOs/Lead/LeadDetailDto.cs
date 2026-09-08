namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadDetailDto
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string Source { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public int? PropertyId { get; set; }

    public string? PropertyTitle { get; set; }

    public int? AssignedUserId { get; set; }

    public string? AssignedUserName { get; set; }

    public List<LeadActivityDto> Activities { get; set; } = new();

    public List<LeadNoteDto> NotesList { get; set; } = new();

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
