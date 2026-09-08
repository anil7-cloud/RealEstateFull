namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadStatusHistoryRequestDto
{
    public int LeadId { get; set; }

    public string PreviousStatus { get; set; } = string.Empty;

    public string NewStatus { get; set; } = string.Empty;

    public string Reason { get; set; } = string.Empty;

    public int? UserId { get; set; }
}
