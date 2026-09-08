namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyContractRequestDto
{
    public int PropertyId { get; set; }

    public int? OwnerId { get; set; }

    public int? TenantId { get; set; }

    public string ContractNumber { get; set; } = string.Empty;

    public string ContractType { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal Amount { get; set; }

    public string Status { get; set; } = string.Empty;
}
