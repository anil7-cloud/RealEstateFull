namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyExpenseRequestDto
{
    public int PropertyId { get; set; }

    public string ExpenseType { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Description { get; set; } = string.Empty;

    public DateTime ExpenseDate { get; set; } = DateTime.UtcNow;

    public int? CreatedByUserId { get; set; }
}
