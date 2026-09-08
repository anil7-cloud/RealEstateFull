namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadSummaryDto
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string Source { get; set; } = string.Empty;

    public decimal PotentialValue { get; set; }

    public DateTime LastActivityDate { get; set; }

    public DateTime CreatedAt { get; set; }
}
