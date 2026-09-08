using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastNotificationService
{
    Task<PropertyAiCustomerSalesLeadForecastNotificationDto> GetNotificationAsync(int userId);

    Task<PropertyAiCustomerSalesLeadForecastNotificationHistoryDto> GetHistoryAsync(int userId);
}
