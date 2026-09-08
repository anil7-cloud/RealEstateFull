using REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyMatchingService
{
    List<PropertyRecommendationDto> MatchProperties(
        int customerId,
        decimal budget,
        string preferredLocation,
        int roomCount);
}
