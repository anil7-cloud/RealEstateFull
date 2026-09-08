namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyInsuranceRequestDto
{
    public int PropertyId { get; set; }

    public string InsuranceCompany { get; set; } = string.Empty;

    public string PolicyNumber { get; set; } = string.Empty;

    public decimal CoverageAmount { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Status { get; set; } = string.Empty;
}
