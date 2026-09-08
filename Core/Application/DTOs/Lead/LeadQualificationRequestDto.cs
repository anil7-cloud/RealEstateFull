namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadQualificationRequestDto
{
    public int LeadId { get; set; }

    public bool IsQualified { get; set; }

    public string QualificationStatus { get; set; } = string.Empty;

    public decimal Score { get; set; }

    public string Notes { get; set; } = string.Empty;

    public int QualifiedByUserId { get; set; }
}
