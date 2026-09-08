using REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Services.Ai;

public class CustomerRankingService : ICustomerRankingService
{
    private readonly ICustomerScoreService _scoreService;

    public CustomerRankingService(
        ICustomerScoreService scoreService)
    {
        _scoreService = scoreService;
    }


    public CustomerScoreDto CalculateCustomerScore(
        int customerId,
        string customerName,
        bool hasPhone,
        bool hasEmail,
        int activityCount,
        int propertyViews)
    {
        var score = _scoreService.CalculateScore(
            hasPhone,
            hasEmail,
            activityCount,
            propertyViews);


        var probability = _scoreService.CalculateProbability(score);


        return new CustomerScoreDto
        {
            CustomerId = customerId,
            CustomerName = customerName,
            Score = score,
            PurchaseProbability = probability,
            Category = GetCategory(score)
        };
    }


    private string GetCategory(int score)
    {
        if (score >= 80)
            return "Sıcak Müşteri";

        if (score >= 50)
            return "Takip Edilecek";

        return "Soğuk Müşteri";
    }
}
