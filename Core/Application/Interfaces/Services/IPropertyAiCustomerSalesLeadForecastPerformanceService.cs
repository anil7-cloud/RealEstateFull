using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastPerformanceService
{
    Task<PropertyAiCustomerSalesLeadForecastPerformanceDto> GetPerformanceAsync(int userId);

    Task<PropertyAiCustomerSalesLeadForecastPerformanceHistoryDto> GetHistoryAsync(int userId);
}
