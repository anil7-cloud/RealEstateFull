using REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface ICustomerBehaviorAnalysisService
{
    CustomerBehaviorDto AnalyzeBehavior(
        int customerId,
        int visitCount,
        int favoriteCount,
        int messageCount,
        int searchCount);
}
