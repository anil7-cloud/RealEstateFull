namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyInspection
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public string InspectorName { get; set; } = string.Empty;

    public DateTime InspectionDate { get; set; }

    public int StructuralScore { get; set; }

    public int ElectricalScore { get; set; }

    public int PlumbingScore { get; set; }

    public int RoofScore { get; set; }

    public int OverallScore { get; set; }

    public string Findings { get; set; } = string.Empty;

    public string Recommendation { get; set; } = string.Empty;

    public string Status { get; set; } = "Completed";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
