using REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Services.Ai;

public class CustomerPredictionService : ICustomerPredictionService
{
    private readonly ICustomerAiAnalysisService _analysisService;

    public CustomerPredictionService(
        ICustomerAiAnalysisService analysisService)
    {
        _analysisService = analysisService;
    }


    public CustomerPredictionDto Predict(
        int customerId,
        string customerName,
        bool hasPhone,
        bool hasEmail,
        int activityCount,
        int propertyViews)
    {
        var score = _analysisService.AnalyzeCustomer(
            customerId,
            customerName,
            hasPhone,
            hasEmail,
            activityCount,
            propertyViews);


        return new CustomerPredictionDto
        {
            CustomerId = score.CustomerId,
            CustomerName = score.CustomerName,
            Score = score.Score,
            Probability = score.PurchaseProbability,
            Prediction =
                score.PurchaseProbability >= 80
                ? "Satın almaya yakın"
                : "Takip edilmeli"
        };
    }
}
