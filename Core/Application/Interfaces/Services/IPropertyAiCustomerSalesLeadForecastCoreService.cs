using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastCoreService
{
    Task<PropertyAiCustomerSalesLeadForecastDto> GetForecastAsync(int userId);

    Task<PropertyAiCustomerSalesLeadForecastAnalysisDto> GetAnalysisAsync(int userId);

    Task<PropertyAiCustomerSalesLeadForecastDashboardDto> GetDashboardAsync(int userId);

    Task<PropertyAiCustomerSalesLeadForecastMetricsDto> GetMetricsAsync(int userId);
}
