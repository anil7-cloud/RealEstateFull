using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyAiCustomerSalesLeadForecastFunnelService
    : IPropertyAiCustomerSalesLeadForecastFunnelService
{
    public Task<PropertyAiCustomerSalesLeadForecastFunnelDto> GetFunnelAsync(
        int userId)
    {
        var result = new PropertyAiCustomerSalesLeadForecastFunnelDto
        {
            UserId = userId
        };

        return Task.FromResult(result);
    }

    public Task<PropertyAiCustomerSalesLeadForecastFunnelHistoryDto> GetHistoryAsync(
        int userId)
    {
        var result = new PropertyAiCustomerSalesLeadForecastFunnelHistoryDto
        {
            UserId = userId
        };

        return Task.FromResult(result);
    }
}
