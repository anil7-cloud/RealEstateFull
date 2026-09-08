using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastAnalyticsService
{
    Task<PropertyAiCustomerSalesLeadForecastFunnelAnalyticsDto> GetAnalyticsAsync(int userId);

    Task<PropertyAiCustomerSalesLeadForecastFunnelAnalyticsHistoryDto> GetHistoryAsync(int userId);
}
