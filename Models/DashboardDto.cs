public class DashboardDto
{
    public int TotalLeads { get; set; }
    public int New { get; set; }
    public int Contacted { get; set; }
    public int Won { get; set; }
    public int Lost { get; set; }

    public double ConversionRate { get; set; }
    public double DropOffRate { get; set; }
    public decimal PredictedRevenue { get; set; }

    public string GrowthTrend { get; set; } = "Stable";
    public int ActivePipeline { get; set; }
}
