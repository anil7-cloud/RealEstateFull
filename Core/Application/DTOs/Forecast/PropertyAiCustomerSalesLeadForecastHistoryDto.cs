namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Forecast;

public class PropertyAiCustomerSalesLeadForecastHistoryDto
{
    public int UserId { get; set; }

    public List<string> History { get; set; } = new();
}
