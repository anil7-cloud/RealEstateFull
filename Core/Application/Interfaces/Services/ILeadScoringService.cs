using REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface ILeadScoringService
{
    CustomerScoreDto CalculateLeadScore(
        int customerId,
        string customerName,
        int visitCount,
        int favoriteCount,
        int messageCount,
        bool hasPhone,
        bool hasEmail);
}
