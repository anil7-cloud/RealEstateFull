namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadConversionFunnel
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public string FunnelStage { get; set; } = string.Empty;

    public int StageOrder { get; set; }

    public decimal ConversionProbability { get; set; }

    public DateTime EnteredAt { get; set; } = DateTime.UtcNow;

    public DateTime? ExitedAt { get; set; }

    public int DurationInDays { get; set; }

    public bool IsCurrentStage { get; set; }

    public bool IsConverted { get; set; }

    public string ExitReason { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
