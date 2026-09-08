namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyTransactionRequestDto
{
    public int PropertyId { get; set; }

    public int BuyerId { get; set; }

    public int SellerId { get; set; }

    public decimal TransactionAmount { get; set; }

    public string TransactionType { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
}
