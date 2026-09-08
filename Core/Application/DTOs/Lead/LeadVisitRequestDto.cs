namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadVisitRequestDto
{
    public int LeadId { get; set; }

    public int PropertyId { get; set; }

    public DateTime VisitDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }
}
