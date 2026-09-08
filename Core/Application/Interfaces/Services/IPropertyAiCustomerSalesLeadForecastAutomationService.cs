using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastAutomationService
{
    Task<PropertyAiCustomerSalesLeadForecastAutomationDto> GetAutomationAsync(int userId);

    Task<PropertyAiCustomerSalesLeadForecastAutomationHistoryDto> GetHistoryAsync(int userId);
}
