using REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyRankingService
{
    List<PropertyRecommendationDto> RankProperties(
        int customerId,
        decimal budget,
        string location,
        int roomCount);
}
