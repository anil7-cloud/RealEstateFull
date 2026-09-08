
namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyExpense

{

    public int Id { get; set; }

    public int PropertyId { get; set; }

    public string ExpenseType { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public DateTime ExpenseDate { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Vendor { get; set; } = string.Empty;

    public bool IsPaid { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}

