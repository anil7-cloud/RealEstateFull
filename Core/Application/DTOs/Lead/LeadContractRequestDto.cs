namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadContractRequestDto
{
    public int LeadId { get; set; }

    public string ContractNumber { get; set; } = string.Empty;

    public string ContractType { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}
