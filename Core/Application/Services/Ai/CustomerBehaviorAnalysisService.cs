using REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Services.Ai;

public class CustomerBehaviorAnalysisService : ICustomerBehaviorAnalysisService
{
    public CustomerBehaviorDto AnalyzeBehavior(
        int customerId,
        int visitCount,
        int favoriteCount,
        int messageCount,
        int searchCount)
    {
        var interestScore = 0;

        interestScore += visitCount * 5;
        interestScore += favoriteCount * 10;
        interestScore += messageCount * 15;
        interestScore += searchCount * 3;


        var behavior = new CustomerBehaviorDto
        {
            CustomerId = customerId,
            InterestScore = interestScore,
            VisitLevel = GetLevel(visitCount),
            PurchaseIntent = CalculateIntent(interestScore)
        };

        return behavior;
    }


    private string GetLevel(int visits)
    {
        if (visits > 20)
            return "Yüksek";

        if (visits > 5)
            return "Orta";

        return "Düşük";
    }


    private int CalculateIntent(int score)
    {
        if (score >= 200)
            return 90;

        if (score >= 100)
            return 70;

        return 40;
    }
}
