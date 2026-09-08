using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastFunnelService
{
    Task<PropertyAiCustomerSalesLeadForecastFunnelDto> GetFunnelAsync(int userId);

    Task<PropertyAiCustomerSalesLeadForecastFunnelHistoryDto> GetHistoryAsync(int userId);
}
