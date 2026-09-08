namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;

public class CustomerScoreDto
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = "";

    public int Score { get; set; }

    public int PurchaseProbability { get; set; }

    public string Category { get; set; } = "";
}
