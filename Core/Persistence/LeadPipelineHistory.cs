namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPipelineHistory
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public string PreviousStage { get; set; } = string.Empty;

    public string CurrentStage { get; set; } = string.Empty;

    public int? ChangedByUserId { get; set; }

    public string ChangeReason { get; set; } = string.Empty;

    public decimal? EstimatedValue { get; set; }

    public decimal? Probability { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    public string Notes { get; set; } = string.Empty;

    public bool IsAutomatic { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
