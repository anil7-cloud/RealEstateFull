using REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Services.Ai;

public class PropertyRankingService : IPropertyRankingService
{
    private readonly IPropertyMatchingService _matchingService;

    public PropertyRankingService(
        IPropertyMatchingService matchingService)
    {
        _matchingService = matchingService;
    }


    public List<PropertyRecommendationDto> RankProperties(
        int customerId,
        decimal budget,
        string location,
        int roomCount)
    {
        var properties = _matchingService.MatchProperties(
            customerId,
            budget,
            location,
            roomCount);


        return properties
            .OrderByDescending(x => x.MatchScore)
            .ToList();
    }
}
