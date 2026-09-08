namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyPaymentRequestDto
{
    public int PropertyId { get; set; }

    public int UserId { get; set; }

    public decimal Amount { get; set; }

    public string PaymentType { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
}
