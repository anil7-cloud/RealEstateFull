using REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface ICustomerRankingService
{
    CustomerScoreDto CalculateCustomerScore(
        int customerId,
        string customerName,
        bool hasPhone,
        bool hasEmail,
        int activityCount,
        int propertyViews);
}
