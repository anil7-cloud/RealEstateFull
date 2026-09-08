namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyRentalRequestDto
{
    public int PropertyId { get; set; }

    public int? TenantId { get; set; }

    public decimal MonthlyRent { get; set; }

    public decimal DepositAmount { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;
}
