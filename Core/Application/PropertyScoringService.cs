namespace REAL_ESTATE_CLEAN.Core.Application;

public class PropertyScoringService
{
    public int CalculateScore(dynamic p)
    {
        int score = 0;

        if (p.IsPremium) score += 50;
        if (p.Price < 1000000) score += 20;
        if (!string.IsNullOrEmpty(p.City)) score += 10;
        if (!string.IsNullOrEmpty(p.Description)) score += 20;

        return score;
    }
}
