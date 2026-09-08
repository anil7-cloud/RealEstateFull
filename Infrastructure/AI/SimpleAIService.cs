namespace REAL_ESTATE_CLEAN.Infrastructure.AI;

public class SimpleAIService
{
    public int CalculateScore(string input)
    {
        return input.Length * 5;
    }
}
