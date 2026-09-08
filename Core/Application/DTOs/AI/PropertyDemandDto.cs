namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;

public class PropertyDemandDto
{
    public string Location { get; set; } = "";

    public int RoomCount { get; set; }

    public int DemandScore { get; set; }

    public string DemandLevel { get; set; } = "";
}
