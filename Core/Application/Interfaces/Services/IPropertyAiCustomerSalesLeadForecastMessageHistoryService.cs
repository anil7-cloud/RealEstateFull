using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastMessageHistoryService
{
    Task<PropertyAiCustomerSalesLeadForecastMessageHistoryDto> GetHistoryAsync(int userId);

    Task<bool> SaveAsync(PropertyAiCustomerSalesLeadForecastMessageHistoryDto dto);
}
