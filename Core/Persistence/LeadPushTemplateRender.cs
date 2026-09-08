namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushTemplateRender
{
    public int Id { get; set; }

    public int LeadPushTemplateId { get; set; }

    public int? LeadPushTaskId { get; set; }

    public string VariablesJson { get; set; } = string.Empty;

    public string RenderedTitle { get; set; } = string.Empty;

    public string RenderedContent { get; set; } = string.Empty;

    public bool IsSuccessful { get; set; }

    public string ErrorMessage { get; set; } = string.Empty;

    public DateTime RenderedAt { get; set; } = DateTime.UtcNow;

    public LeadPushTemplate? Template { get; set; }

    public LeadPushTask? Task { get; set; }
}
