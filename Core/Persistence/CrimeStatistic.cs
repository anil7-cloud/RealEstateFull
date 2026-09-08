namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class CrimeStatistic
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public string RegionName { get; set; } = string.Empty;

    public int TheftCases { get; set; }

    public int AssaultCases { get; set; }

    public int TrafficAccidents { get; set; }

    public double SafetyScore { get; set; }

    public int Year { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
