namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Forecast;

public class PropertyAiCustomerSalesLeadForecastDashboardDto
{
    public int UserId { get; set; }

    public int TotalCustomers { get; set; }

    public decimal ExpectedRevenue { get; set; }
}
