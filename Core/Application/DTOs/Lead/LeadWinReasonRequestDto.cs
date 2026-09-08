namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadWinReasonRequestDto
{
    public int LeadId { get; set; }

    public string Reason { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int? UserId { get; set; }
}
