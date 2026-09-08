using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastOptimizationService
{
    Task<PropertyAiCustomerSalesLeadForecastOptimizationDto> GetOptimizationAsync(int userId);

    Task<PropertyAiCustomerSalesLeadForecastOptimizationHistoryDto> GetHistoryAsync(int userId);
}
