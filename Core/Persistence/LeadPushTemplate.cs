namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushTemplate
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string Channel { get; set; } = "Push";

    public bool IsActive { get; set; } = true;

    public string Language { get; set; } = "tr";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
