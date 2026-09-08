namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyInvoiceRequestDto
{
    public int PropertyId { get; set; }

    public int UserId { get; set; }

    public string InvoiceNumber { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;
}
