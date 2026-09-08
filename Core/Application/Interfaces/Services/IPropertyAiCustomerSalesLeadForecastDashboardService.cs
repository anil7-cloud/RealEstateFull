using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastDashboardService
{
    Task<PropertyAiCustomerSalesLeadForecastDashboardDto> GetDashboardAsync(int userId);

    Task<PropertyAiCustomerSalesLeadForecastDashboardHistoryDto> GetHistoryAsync(int userId);
}
