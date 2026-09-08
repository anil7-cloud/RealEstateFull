using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastMetricsService
{
    Task<PropertyAiCustomerSalesLeadForecastMetricsDto> GetMetricsAsync(int userId);

    Task<PropertyAiCustomerSalesLeadForecastMetricsHistoryDto> GetHistoryAsync(int userId);
}
