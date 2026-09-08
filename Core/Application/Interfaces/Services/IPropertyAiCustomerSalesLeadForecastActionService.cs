using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastActionService
{
    Task<PropertyAiCustomerSalesLeadForecastActionDto> GetActionAsync(int userId);

    Task<PropertyAiCustomerSalesLeadForecastActionHistoryDto> GetHistoryAsync(int userId);
}
