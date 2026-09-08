namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface ICustomerScoreService
{
    int CalculateScore(
        bool hasPhone,
        bool hasEmail,
        int activityCount,
        int propertyViews
    );


    int CalculateProbability(int score);
}
