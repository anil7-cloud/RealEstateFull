using REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface ICustomerRecommendationService
{
    string GetRecommendation(CustomerBehaviorDto behavior);

    List<string> GetSuggestedActions(CustomerBehaviorDto behavior);
}
