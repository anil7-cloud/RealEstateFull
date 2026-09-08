namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushTemplateVersion
{
    public int Id { get; set; }

    public int LeadPushTemplateId { get; set; }

    public int Version { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string ChangeLog { get; set; } = string.Empty;

    public bool IsPublished { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int? CreatedByUserId { get; set; }

    public LeadPushTemplate? Template { get; set; }
}
