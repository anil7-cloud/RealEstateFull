namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadNotificationTemplateVariable
{
    public int Id { get; set; }

    public int TemplateId { get; set; }

    public string Key { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public LeadNotificationTemplate? Template { get; set; }
}
