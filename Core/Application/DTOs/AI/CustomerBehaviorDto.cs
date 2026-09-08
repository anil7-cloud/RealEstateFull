namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;

public class CustomerBehaviorDto
{
    public int CustomerId { get; set; }

    public int InterestScore { get; set; }

    public string VisitLevel { get; set; } = "";

    public int PurchaseIntent { get; set; }
}
