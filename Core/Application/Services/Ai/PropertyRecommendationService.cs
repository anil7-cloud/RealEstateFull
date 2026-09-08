using REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Services.Ai;

public class PropertyRecommendationService : IPropertyRecommendationService
{
    public List<PropertyRecommendationDto> RecommendProperties(
        int customerId,
        decimal minPrice,
        decimal maxPrice,
        string location,
        int roomCount)
    {
        var recommendations = new List<PropertyRecommendationDto>();

        recommendations.Add(new PropertyRecommendationDto
        {
            CustomerId = customerId,
            PropertyTitle = $"{location} {roomCount}+1 Daire",
            Price = (minPrice + maxPrice) / 2,
            MatchScore = 90
        });

        return recommendations;
    }
}
