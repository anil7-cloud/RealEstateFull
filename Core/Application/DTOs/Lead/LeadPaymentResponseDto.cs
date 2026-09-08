namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadPaymentResponseDto
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public decimal Amount { get; set; }

    public string PaymentType { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime PaymentDate { get; set; }

    public DateTime CreatedAt { get; set; }
}
