namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadNote
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int? PropertyId { get; set; }

    public int? UserId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string NoteType { get; set; } = "Internal";

    public string Priority { get; set; } = "Normal";

    public bool IsPinned { get; set; }

    public bool IsArchived { get; set; }

    public string Tags { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
