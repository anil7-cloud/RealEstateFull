using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyAiCustomerSalesLeadForecastMessageService
    : IPropertyAiCustomerSalesLeadForecastMessageService
{
    public Task<PropertyAiCustomerSalesLeadForecastMessageDto> GetMessageAsync(
        int userId)
    {
        return Task.FromResult(
            new PropertyAiCustomerSalesLeadForecastMessageDto
            {
                UserId = userId,
                PropertyId = 0,
                Message = "AI customer follow-up message generated",
                SenderType = "AI",
                IsRead = false
            });
    }

    public Task<PropertyAiCustomerSalesLeadForecastMessageHistoryDto> GetHistoryAsync(
        int userId)
    {
        return Task.FromResult(
            new PropertyAiCustomerSalesLeadForecastMessageHistoryDto
            {
                UserId = userId
            });
    }
}
