using REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface ICustomerAiAnalysisService
{
    CustomerScoreDto AnalyzeCustomer(
        int customerId,
        string customerName,
        bool hasPhone,
        bool hasEmail,
        int activityCount,
        int propertyViews);


    string GetRecommendation(CustomerScoreDto score);
}
