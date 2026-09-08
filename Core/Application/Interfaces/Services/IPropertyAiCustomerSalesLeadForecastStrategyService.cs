using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastStrategyService
{
    Task<PropertyAiCustomerSalesLeadForecastStrategyDto> GetStrategyAsync(int userId);

    Task<PropertyAiCustomerSalesLeadForecastStrategyHistoryDto> GetHistoryAsync(int userId);
}
