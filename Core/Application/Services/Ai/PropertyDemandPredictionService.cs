using REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Services.Ai;

public class PropertyDemandPredictionService : IPropertyDemandPredictionService
{
    public PropertyDemandDto PredictDemand(
        string location,
        int roomCount,
        decimal averagePrice,
        int viewCount,
        int favoriteCount)
    {
        var demandScore = 0;

        demandScore += viewCount * 2;
        demandScore += favoriteCount * 5;

        if (averagePrice < 5000000)
            demandScore += 20;

        if (roomCount == 3)
            demandScore += 15;


        return new PropertyDemandDto
        {
            Location = location,
            RoomCount = roomCount,
            DemandScore = demandScore,
            DemandLevel = GetDemandLevel(demandScore)
        };
    }


    private string GetDemandLevel(int score)
    {
        if (score >= 100)
            return "Yüksek Talep";

        if (score >= 50)
            return "Orta Talep";

        return "Düşük Talep";
    }
}
