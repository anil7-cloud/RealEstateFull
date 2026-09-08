namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadNoteDto
{
    public int LeadId { get; set; }

    public string Note { get; set; } = string.Empty;

    public int? UserId { get; set; }
}
