namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadVisit
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int PropertyId { get; set; }

    public int? UserId { get; set; }

    public DateTime VisitDate { get; set; }

    // Scheduled, Completed, Cancelled, NoShow
    public string Status { get; set; } = "Scheduled";

    public int Rating { get; set; }

    public bool Interested { get; set; }

    public bool WantsSecondVisit { get; set; }

    public string Feedback { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
