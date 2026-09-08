using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastMessageService
{
    Task<PropertyAiCustomerSalesLeadForecastMessageDto> GetMessageAsync(int userId);

    Task<PropertyAiCustomerSalesLeadForecastMessageHistoryDto> GetHistoryAsync(int userId);
}
