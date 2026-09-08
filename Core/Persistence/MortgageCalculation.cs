namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class MortgageCalculation
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PropertyId { get; set; }

    public decimal PropertyPrice { get; set; }

    public decimal DownPayment { get; set; }

    public decimal LoanAmount { get; set; }

    public double InterestRate { get; set; }

    public int LoanTermMonths { get; set; }

    public decimal MonthlyPayment { get; set; }

    public decimal TotalPayment { get; set; }

    public decimal TotalInterest { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
