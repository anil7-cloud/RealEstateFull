using REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Services.Ai;

public class CustomerAiAnalysisService : ICustomerAiAnalysisService
{
    private readonly ICustomerRankingService _rankingService;

    public CustomerAiAnalysisService(
        ICustomerRankingService rankingService)
    {
        _rankingService = rankingService;
    }


    public CustomerScoreDto AnalyzeCustomer(
        int customerId,
        string customerName,
        bool hasPhone,
        bool hasEmail,
        int activityCount,
        int propertyViews)
    {
        var result = _rankingService.CalculateCustomerScore(
            customerId,
            customerName,
            hasPhone,
            hasEmail,
            activityCount,
            propertyViews);

        return result;
    }


    public string GetRecommendation(CustomerScoreDto score)
    {
        if (score.Score >= 80)
            return "Müşteriyle hemen iletişime geç";

        if (score.Score >= 50)
            return "Takip listesine ekle";

        return "Uzun vadeli müşteri olarak değerlendir";
    }
}
