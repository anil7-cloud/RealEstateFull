namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadKpiRequestDto
{
    public string KpiName { get; set; } = string.Empty;

    public decimal TargetValue { get; set; }

    public int? UserId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}
