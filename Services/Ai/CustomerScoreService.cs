using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Services.Ai;

public class CustomerScoreService : ICustomerScoreService
{
    public int CalculateScore(
        bool hasPhone,
        bool hasEmail,
        int activityCount,
        int propertyViews)
    {
        int score = 0;

        if (hasPhone)
            score += 20;

        if (hasEmail)
            score += 20;

        if (activityCount > 10)
            score += 30;

        if (propertyViews > 5)
            score += 30;

        return score;
    }


    public int CalculateProbability(int score)
    {
        if (score >= 80)
            return 90;

        if (score >= 50)
            return 70;

        return 40;
    }
}
