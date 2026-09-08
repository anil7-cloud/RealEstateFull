using REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Services.Ai;

public class PropertyMatchingService : IPropertyMatchingService
{
    public List<PropertyRecommendationDto> MatchProperties(
        int customerId,
        decimal budget,
        string preferredLocation,
        int roomCount)
    {
        var results = new List<PropertyRecommendationDto>();

        results.Add(new PropertyRecommendationDto
        {
            CustomerId = customerId,
            PropertyTitle = $"{preferredLocation} {roomCount}+1 Uygun Daire",
            Price = budget,
            MatchScore = 85,
            Reason = "Konum ve bütçe kriterleri uyumlu"
        });

        return results;
    }
}
