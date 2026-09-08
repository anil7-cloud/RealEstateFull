using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastActivityService
{
    Task<PropertyAiCustomerSalesLeadForecastActivityDto> GetActivityAsync(int userId);

    Task<PropertyAiCustomerSalesLeadForecastActivityHistoryDto> GetHistoryAsync(int userId);
}
