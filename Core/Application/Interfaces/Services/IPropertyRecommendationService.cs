using REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyRecommendationService
{
    List<PropertyRecommendationDto> RecommendProperties(
        int customerId,
        decimal minPrice,
        decimal maxPrice,
        string location,
        int roomCount);
}
