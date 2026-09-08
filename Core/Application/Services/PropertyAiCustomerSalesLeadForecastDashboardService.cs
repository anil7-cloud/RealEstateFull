using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyAiCustomerSalesLeadForecastDashboardService
    : IPropertyAiCustomerSalesLeadForecastDashboardService
{
    public Task<PropertyAiCustomerSalesLeadForecastDashboardDto> GetDashboardAsync(
        int userId)
    {
        var result = new PropertyAiCustomerSalesLeadForecastDashboardDto
        {
            UserId = userId
        };

        return Task.FromResult(result);
    }

    public Task<PropertyAiCustomerSalesLeadForecastDashboardHistoryDto> GetHistoryAsync(
        int userId)
    {
        var result = new PropertyAiCustomerSalesLeadForecastDashboardHistoryDto
        {
            UserId = userId
        };

        return Task.FromResult(result);
    }
}
