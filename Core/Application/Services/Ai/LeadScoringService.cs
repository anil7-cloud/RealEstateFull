using REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Services.Ai;

public class LeadScoringService : ILeadScoringService
{
    public CustomerScoreDto CalculateLeadScore(
        int customerId,
        string customerName,
        int visitCount,
        int favoriteCount,
        int messageCount,
        bool hasPhone,
        bool hasEmail)
    {
        int score = 0;

        score += visitCount * 5;
        score += favoriteCount * 10;
        score += messageCount * 15;

        if (hasPhone)
            score += 20;

        if (hasEmail)
            score += 20;


        var probability = CalculateProbability(score);


        return new CustomerScoreDto
        {
            CustomerId = customerId,
            CustomerName = customerName,
            Score = score,
            PurchaseProbability = probability,
            Category = GetCategory(score)
        };
    }


    private int CalculateProbability(int score)
    {
        if (score >= 100)
            return 90;

        if (score >= 50)
            return 70;

        return 40;
    }


    private string GetCategory(int score)
    {
        if (score >= 100)
            return "Sıcak Lead";

        if (score >= 50)
            return "Orta Lead";

        return "Soğuk Lead";
    }
}
