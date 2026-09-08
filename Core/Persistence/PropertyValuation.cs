namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyValuation
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public decimal AreaSquareMeters { get; set; }

    public int RoomCount { get; set; }

    public int BuildingAge { get; set; }

    public double LocationScore { get; set; }

    public decimal AverageSquareMeterPrice { get; set; }

    public decimal EstimatedValue { get; set; }

    public string Currency { get; set; } = "TRY";

    public DateTime ValuationDate { get; set; } = DateTime.UtcNow;
}
