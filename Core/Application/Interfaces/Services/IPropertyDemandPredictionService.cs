using REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyDemandPredictionService
{
    PropertyDemandDto PredictDemand(
        string location,
        int roomCount,
        decimal averagePrice,
        int viewCount,
        int favoriteCount);
}
