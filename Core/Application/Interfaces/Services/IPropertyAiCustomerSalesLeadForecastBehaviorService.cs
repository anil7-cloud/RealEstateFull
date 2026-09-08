using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastBehaviorService
{
    Task<PropertyAiCustomerSalesLeadForecastBehaviorDto> GetBehaviorAsync(int userId);

    Task<PropertyAiCustomerSalesLeadForecastBehaviorHistoryDto> GetHistoryAsync(int userId);
}
