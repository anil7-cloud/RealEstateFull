namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadInvoiceResponseDto
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public string InvoiceNumber { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime InvoiceDate { get; set; }

    public DateTime CreatedAt { get; set; }
}
