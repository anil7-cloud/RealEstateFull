
namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyIncome

{

    public int Id { get; set; }

    public int PropertyId { get; set; }

    public string IncomeType { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public DateTime IncomeDate { get; set; }

    public string PayerName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsReceived { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}

