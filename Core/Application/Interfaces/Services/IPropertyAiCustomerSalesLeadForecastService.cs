using REAL_ESTATE_CLEAN.Core.Application.DTOs.Forecast;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastService
{
    Task<PropertyAiCustomerSalesLeadForecastDto> GetForecastAsync(int userId);

    Task<PropertyAiCustomerSalesLeadForecastHistoryDto> GetForecastHistoryAsync(int userId);

    Task<PropertyAiCustomerSalesLeadForecastAnalysisDto> AnalyzeAsync(int userId);

    Task<PropertyAiCustomerSalesLeadForecastDashboardDto> GetDashboardAsync(int userId);
}
