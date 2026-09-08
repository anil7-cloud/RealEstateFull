namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;

public class MarketAnalysisDto
{
    public string Location { get; set; } = "";

    public decimal AveragePrice { get; set; }

    public int ListingCount { get; set; }

    public int SoldCount { get; set; }

    public int DemandRate { get; set; }

    public string MarketStatus { get; set; } = "";
}
